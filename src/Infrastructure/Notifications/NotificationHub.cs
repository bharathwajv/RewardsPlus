﻿using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using RewardsPlus.Application.Common.Exceptions;
using RewardsPlus.Application.Common.Interfaces;

namespace RewardsPlus.Infrastructure.Notifications;

[Authorize]
public class NotificationHub : Hub, ITransientService
{
    private readonly ITenantInfo? _currentTenant;
    private readonly ILogger<NotificationHub> _logger;

    public NotificationHub(ITenantInfo? currentTenant, ILogger<NotificationHub> logger)
    {
        _currentTenant = currentTenant;
        _logger = logger;
    }

    public override async Task OnConnectedAsync()
    {
        if (_currentTenant is null)
        {
            throw new UnauthorizedException("Authentication Failed.");
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, $"GroupTenant-{_currentTenant.Id}");

        await base.OnConnectedAsync();

        _logger.LogInformation("A client connected to NotificationHub: {connectionId}", Context.ConnectionId);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"GroupTenant-{_currentTenant!.Id}");

        await base.OnDisconnectedAsync(exception);

        _logger.LogInformation("A client disconnected from NotificationHub: {connectionId}", Context.ConnectionId);
    }
}