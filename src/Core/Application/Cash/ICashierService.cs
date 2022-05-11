namespace RewardsPlus.Application.Cash;

public interface ICashierService : ITransientService
{
    Task<List<CashDto>> GetAllAsync();
    Task<bool> ExistsWithIdAsync(string id);
    Task<CashDto> GetByIdAsync(string id);
    Task<string> GiftAsync(GiftCashRequest request, CancellationToken cancellationToken);
    Task<string> RedeemAsync(RedeemCashRequest request, CancellationToken cancellationToken);
    Task<string> BuyAsync(BuyCashRequest request, CancellationToken cancellationToken);
    Task<string> SeedAsync(string seedUserEmail, string seedUserId, double amountToSeed);
    Task<double> GetBalanceAsync();
}