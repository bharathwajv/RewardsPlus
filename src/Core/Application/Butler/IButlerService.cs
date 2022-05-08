namespace RewardsPlus.Application.Token;

public interface IButlerService
{
    Task<string> PlaceOrder(BuyProductRequest productId, CancellationToken cancellationToken);
}