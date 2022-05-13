﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using RewardsPlus.Application.Common.Exceptions;
using RewardsPlus.Application.Common.Mailing;
using RewardsPlus.Application.Identity;
using RewardsPlus.Application.Identity.Users;
using RewardsPlus.Domain.Common;
using RewardsPlus.Domain.Identity;
using RewardsPlus.Shared.Authorization;
using System.Security.Claims;

namespace RewardsPlus.Infrastructure.Identity;

internal partial class UserService
{
    /// <summary>
    /// This is used when authenticating with AzureAd.
    /// The local user is retrieved using the objectidentifier claim present in the ClaimsPrincipal.
    /// If no such claim is found, an InternalServerException is thrown.
    /// If no user is found with that ObjectId, a new one is created and populated with the values from the ClaimsPrincipal.
    /// If a role claim is present in the principal, and the user is not yet in that roll, then the user is added to that role.
    /// </summary>
    public async Task<string> GetOrCreateFromPrincipalAsync(ClaimsPrincipal principal)
    {
        string? objectId = principal.GetObjectId();
        if (string.IsNullOrWhiteSpace(objectId))
        {
             throw new InternalServerException(_t["Invalid objectId"]);
        }

        var user = await _userManager.Users.Where(u => u.ObjectId == objectId).FirstOrDefaultAsync()
            ?? await CreateOrUpdateFromPrincipalAsync(principal);

        if (principal.FindFirstValue(ClaimTypes.Role) is string role &&
            await _roleManager.RoleExistsAsync(role) &&
            !await _userManager.IsInRoleAsync(user, role))
        {
            await _userManager.AddToRoleAsync(user, role);
        }

        return user.Id;
    }

    private async Task<ApplicationUser> CreateOrUpdateFromPrincipalAsync(ClaimsPrincipal principal)
    {
        string? email = principal.FindFirstValue(ClaimTypes.Upn);
        string? username = principal.GetDisplayName();
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(username))
        {
            throw new InternalServerException(string.Format(_t["Username or Email not valid."]));
        }

        var user = await _userManager.FindByNameAsync(username);
        if (user is not null && !string.IsNullOrWhiteSpace(user.ObjectId))
        {
            throw new InternalServerException(string.Format(_t["Username {0} is already taken."], username));
        }

        if (user is null)
        {
            user = await _userManager.FindByEmailAsync(email);
            if (user is not null && !string.IsNullOrWhiteSpace(user.ObjectId))
            {
                throw new InternalServerException(string.Format(_t["Email {0} is already taken."], email));
            }
        }

        IdentityResult? result;
        if (user is not null)
        {
            user.ObjectId = principal.GetObjectId();
            result = await _userManager.UpdateAsync(user);

            await _events.PublishAsync(new ApplicationUserUpdatedEvent(user.Id));
        }
        else
        {
            user = new ApplicationUser
            {
                ObjectId = principal.GetObjectId(),
                Email = email,
                NormalizedEmail = email.ToUpperInvariant(),
                UserName = username,
                NormalizedUserName = username.ToUpperInvariant(),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true
            };
            result = await _userManager.CreateAsync(user);

            await _events.PublishAsync(new ApplicationUserCreatedEvent(user.Id));
        }

        if (!result.Succeeded)
        {
            throw new InternalServerException(_t["Validation Errors Occurred."], result.GetErrors(_t));
        }

        return user;
    }

    public async Task<string> CreateAsync(CreateUserRequest request, string origin)
    {
        var user = new ApplicationUser
        {
            Email = request.Email,
            UserName = request.UserName,
            PhoneNumber = request.PhoneNumber,
            IsActive = true
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            throw new InternalServerException(_t["Validation Errors Occurred."], result.GetErrors(_t));
        }

        await _userManager.AddToRoleAsync(user, FSHRoles.Basic);

        var messages = new List<string> { string.Format(_t["User {0} Registered."], user.UserName) };

        if (_securitySettings.RequireConfirmedAccount && !string.IsNullOrEmpty(user.Email))
        {
            await SendVerificationMail(origin, user, messages);
        }

        await _events.PublishAsync(new ApplicationUserCreatedEvent(user.Id));

        return string.Join(Environment.NewLine, messages);
    }

    private async Task SendVerificationMail(string origin, ApplicationUser user, List<string> messages)
    {
        string emailVerificationUri = await GetEmailVerificationUriAsync(user, origin);
        RegisterUserEmailModel eMailModel = new RegisterUserEmailModel()
        {
            Email = user.Email,
            UserName = user.UserName,
            Url = emailVerificationUri
        };
        var mailRequest = new MailRequest(
            new List<string> { user.Email },
             _t["Confirm Registration"],
                _templateService.GenerateEmailTemplate("email-confirmation", eMailModel));
            _jobService.Enqueue(() => _mailService.SendAsync(mailRequest, CancellationToken.None));
            messages.Add(_t[$"Please check {user.Email} to verify your account!"]);
    }

    public async Task UpdateAsync(UpdateUserRequest request, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        _ = user ?? throw new NotFoundException(_t["User Not Found."]);

        string currentImage = user.ImageUrl ?? string.Empty;
        if (request.Image != null || request.DeleteCurrentImage)
        {
            user.ImageUrl = await _fileStorage.UploadAsync<ApplicationUser>(request.Image, FileType.Image);
            if (request.DeleteCurrentImage && !string.IsNullOrEmpty(currentImage))
            {
                string root = Directory.GetCurrentDirectory();
                _fileStorage.Remove(Path.Combine(root, currentImage));
            }
        }

        user.UserName = request.UserName;
        user.PhoneNumber = request.PhoneNumber;
        string phoneNumber = await _userManager.GetPhoneNumberAsync(user);
        if (request.PhoneNumber != phoneNumber)
        {
            await _userManager.SetPhoneNumberAsync(user, request.PhoneNumber);
        }

        var result = await _userManager.UpdateAsync(user);

        await _signInManager.RefreshSignInAsync(user);

        await _events.PublishAsync(new ApplicationUserUpdatedEvent(user.Id));

        if (!result.Succeeded)
        {
            throw new InternalServerException(_t["Update profile failed"], result.GetErrors(_t));
        }
    }
}
