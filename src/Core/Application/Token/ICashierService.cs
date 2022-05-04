namespace RewardsPlus.Application.Token;

public interface ICashierService : ITransientService
{
    Task<List<TokenDto>> GetAllAsync();
    Task<bool> ExistsWithIdAsync(string id);
    Task<TokenDto> GetByIdAsync(string id);
    Task<string> GiftAsync(GiftTokensRequest request, CancellationToken cancellationToken);
    Task<string> RedeemAsync(RedeemTokensRequest request, CancellationToken cancellationToken);
    Task<string> BuyAsync(BuyTokensRequest request, CancellationToken cancellationToken);
    Task<string> SeedAsync(string seedUserEmail, string seedUserId, double amountToSeed);
}