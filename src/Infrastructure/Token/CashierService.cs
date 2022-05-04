using Mapster;
using Microsoft.EntityFrameworkCore;
using RewardsPlus.Application.Common.Interfaces;
using RewardsPlus.Application.Token;
using RewardsPlus.Domain.Catalog;
using RewardsPlus.Infrastructure.Identity;
using RewardsPlus.Infrastructure.Persistence.Context;

namespace RewardsPlus.Infrastructure.Multitenancy;

internal class CashierService : ICashierService
{
    private readonly ApplicationDbContext _context;
    private readonly ICurrentUser _currentUser;
    public CashierService(ICurrentUser currentUser, ApplicationDbContext context) => (_currentUser, _context) = (currentUser, context);

    public async Task<List<TokenDto>> GetAllAsync()
    {
        var tokens = await _context.Tokens
           .OrderByDescending(a => a.UserEmail)
           .Take(250)
           .ToListAsync();

        return tokens.Adapt<List<TokenDto>>();
    }

    public async Task<bool> ExistsWithIdAsync(string id) =>
       await _context.Tokens.AnyAsync(x => x.UserId == id);

    public async Task<TokenDto> GetByIdAsync(string id)
    {
        var tokenInfo = _context.Tokens?.ToList()?.Find(x => x.UserId == id);
        return tokenInfo?.Adapt<TokenDto>();
    }

    public async Task<string> GiftAsync(GiftTokensRequest request, CancellationToken cancellationToken)
    {
        var toUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.ToEmailId, cancellationToken: cancellationToken);

        var curentUserTokenInfo = _context.Tokens?.ToList()?.Find(x => x.UserEmail == _currentUser.GetUserEmail());

        ValidateBeforeGifting(request, toUser, curentUserTokenInfo);

        var toUserTokenInfo = _context.Tokens?.ToList()?.Find(x => x.UserEmail == toUser?.Email);

        if (toUserTokenInfo == null)
        {
            _context.Tokens?.AddAsync(new Token(request.Amount, toUser.Id, toUser.Email), cancellationToken);
        }
        else
        {
            toUserTokenInfo.Update(toUserTokenInfo.Balance + request.Amount);
        }

        await _context.SaveChangesAsync(cancellationToken);

        //if success deduct
        curentUserTokenInfo?.Update(curentUserTokenInfo.Balance - request.Amount);
        await _context.SaveChangesAsync(cancellationToken);

        return string.Empty;
    }

    private static void ValidateBeforeGifting(GiftTokensRequest request, ApplicationUser? toUser, Token? curentUserTokenInfo)
    {
        if (toUser == null)
            throw new Exception("User not found");

        if (curentUserTokenInfo != null && curentUserTokenInfo.UserId == toUser.Id)
            throw new Exception("You can't gift to yourself");

        if (curentUserTokenInfo != null && curentUserTokenInfo.Balance < request.Amount)
            throw new Exception("Insufficient balance");
    }

    //buy tokens
    public async Task<string> BuyAsync(BuyTokensRequest request, CancellationToken cancellationToken)
    {
        var curentUserTokenInfo = _context.Tokens?.ToList()?.Find(x => x.UserEmail == _currentUser.GetUserEmail());

        if (curentUserTokenInfo == null)
        {
            _context.Tokens?.AddAsync(new Token(request.Amount, _currentUser.GetUserId().ToString(), _currentUser?.GetUserEmail()), cancellationToken);
        }
        else
        {
            curentUserTokenInfo.Update(curentUserTokenInfo.Balance + request.Amount);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return string.Empty;
    }
    //redeem tokens
    public async Task<string> RedeemAsync(RedeemTokensRequest request, CancellationToken cancellationToken)
    {
        var curentUserTokenInfo = _context.Tokens?.ToList()?.Find(x => x.UserEmail == _currentUser.GetUserEmail());

        ValidateBeforeRedeem(request, curentUserTokenInfo);

        curentUserTokenInfo?.Update(curentUserTokenInfo.Balance - request.Amount);

        await _context.SaveChangesAsync(cancellationToken);

        return string.Empty;
    }

    private static void ValidateBeforeRedeem(RedeemTokensRequest request, Token? curentUserTokenInfo)
    {
        if (curentUserTokenInfo == null)
        {
            throw new Exception("You don't have any tokens");
        }

        if (curentUserTokenInfo.Balance < request.Amount)
        {
            throw new Exception("Insufficient balance");
        }
    }

    //seed for devlopment
    public async Task<string> SeedAsync(string seedUserEmail, string seedUserId, double amountToSeed)
    {
        var curentUserTokenInfo = _context.Tokens?.ToList()?.Find(x => x.UserEmail == seedUserEmail);

        if (curentUserTokenInfo == null)
        {
            _context.Tokens?.AddAsync(new Token(amountToSeed, seedUserId, seedUserEmail));
            await _context.SaveChangesAsync();
        }
        return string.Empty;
    }

}