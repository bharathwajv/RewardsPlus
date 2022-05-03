using Mapster;
using Microsoft.EntityFrameworkCore;
using RewardsPlus.Application.Common.Interfaces;
using RewardsPlus.Application.Token;
using RewardsPlus.Infrastructure.Persistence.Context;

namespace RewardsPlus.Infrastructure.Multitenancy;

internal class TokenService : ITokenService
{
    private readonly ApplicationDbContext _context;
    private readonly ICurrentUser _currentUser;
    public TokenService(ICurrentUser currentUser, ApplicationDbContext context) => (_currentUser, _context) = (currentUser, context);

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
        var mailId = _currentUser.GetUserEmail();
        var tst = _context.Tokens?.ToList()?.Find(x => x.UserEmail == mailId);

        var tst2 = _context.Tokens?.ToList()?.Find(x => x.UserEmail == request?.ToEmailId);

        // if(tst.Balance )
        // Balance += request.Amount

        _context.Tokens?.AddAsync(new(request.Amount, request?.ToEmailId, "user@root.com"), cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
        return string.Empty;
    }
}