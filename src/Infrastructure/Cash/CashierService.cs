using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    private readonly ILogger<CashierService> _logger;
    public CashierService(ICurrentUser currentUser, ApplicationDbContext context, ILogger<CashierService> logger) => (_currentUser, _context, _logger) = (currentUser, context, logger);

    public async Task<List<CashDto>> GetAllAsync()
    {
        var tokens = await _context.Cash
           .OrderByDescending(a => a.UserEmail)
           .Take(250)
           .ToListAsync();

        return tokens.Adapt<List<CashDto>>();
    }

    public async Task<bool> ExistsWithIdAsync(string id) =>
       await _context.Cash.AnyAsync(x => x.UserId == id);

    public async Task<CashDto> GetByIdAsync(string id)
    {
        var tokenInfo = _context.Cash?.ToList()?.Find(x => x.UserId == id);
        return tokenInfo?.Adapt<CashDto>();
    }

    public async Task<string> GiftAsync(GiftCashRequest request, CancellationToken cancellationToken)
    {
        var toUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.ToEmailId, cancellationToken: cancellationToken);

        var curentUserTokenInfo = _context.Cash?.ToList()?.Find(x => x.UserEmail == _currentUser.GetUserEmail());

        ValidateBeforeGifting(request, toUser, curentUserTokenInfo);

        var toUserTokenInfo = _context.Cash?.ToList()?.Find(x => x.UserEmail == toUser?.Email);

        if (toUserTokenInfo == null)
        {
            _context.Cash?.AddAsync(new Cash(request.Amount, toUser.Id, toUser.Email), cancellationToken);
        }
        else
        {
            toUserTokenInfo.Update(toUserTokenInfo.Balance + request.Amount);
        }

        await _context.SaveChangesAsync(cancellationToken);

        //if success deduct
        double newBalance = curentUserTokenInfo.Balance - request.Amount;
        curentUserTokenInfo?.Update(newBalance);
        await _context.SaveChangesAsync(cancellationToken);

        return newBalance.ToString();
    }

    private static void ValidateBeforeGifting(GiftCashRequest request, ApplicationUser? toUser, Cash? curentUserTokenInfo)
    {
        if (toUser == null)
            throw new Exception("User not found");

        if (curentUserTokenInfo != null && curentUserTokenInfo.UserId == toUser.Id)
            throw new Exception("You can't gift to yourself");

        if (curentUserTokenInfo != null && curentUserTokenInfo.Balance < request.Amount)
            throw new Exception("Insufficient balance");
    }

    //buy tokens
    public async Task<string> BuyAsync(BuyCashRequest request, CancellationToken cancellationToken)
    {
        var curentUserTokenInfo = _context.Cash?.ToList()?.Find(x => x.UserEmail == _currentUser.GetUserEmail());
        double newBalance;
        if (curentUserTokenInfo == null)
        {
            newBalance = request.Amount;
            _context.Cash?.AddAsync(new Cash(newBalance, _currentUser.GetUserId().ToString(), _currentUser?.GetUserEmail()), cancellationToken);
        }
        else
        {
            newBalance = curentUserTokenInfo.Balance + request.Amount;
            curentUserTokenInfo.Update(newBalance);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return newBalance.ToString();
    }

    //redeem tokens
    public async Task<string> RedeemAsync(RedeemCashRequest request, CancellationToken cancellationToken)
    {
        var curentUserTokenInfo = _context.Cash?.ToList()?.Find(x => x.UserEmail == _currentUser.GetUserEmail());

        ValidateBeforeRedeem(request, curentUserTokenInfo);

        curentUserTokenInfo?.Update(curentUserTokenInfo.Balance - request.Amount);

        await _context.SaveChangesAsync(cancellationToken);

        return string.Empty;
    }

    private static void ValidateBeforeRedeem(RedeemCashRequest request, Cash? curentUserTokenInfo)
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
        var curentUserTokenInfo = _context.Cash?.ToList()?.Find(x => x.UserEmail == seedUserEmail);

        if (curentUserTokenInfo == null)
        {
            _context.Cash?.AddAsync(new Cash(amountToSeed, seedUserId, seedUserEmail));
            await _context.SaveChangesAsync();
            _logger.LogInformation("Seeding Cash {amountToSeed} to user '{seedUserEmail}'.", amountToSeed, seedUserEmail);
        }

        return string.Empty;
    }

    //get balance
    public async Task<double> GetBalanceAsync()
    {
        var curentUserTokenInfo = _context.Cash?.ToList()?.Find(x => x.UserEmail == _currentUser.GetUserEmail());
        return curentUserTokenInfo?.Balance ?? 0;
    }
}