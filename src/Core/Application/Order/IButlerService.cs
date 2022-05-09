namespace RewardsPlus.Application.Order;

public interface IButlerService : ITransientService
{
    Task<string> PlaceOrder(BuyProductRequest productId, CancellationToken cancellationToken);
}