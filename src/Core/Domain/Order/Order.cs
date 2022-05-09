namespace RewardsPlus.Domain.Order;

public class Order : AuditableEntity, IAggregateRoot
{
    public string ProductId { get; private set; }
    public string UserEmail { get; private set; }

    public Order(string productId, string userEmail)
    {
        ProductId = productId;
        UserEmail = userEmail;
    }

    public Order Update(string productId, string userEmail)
    {
        if (productId is not null && ProductId?.Equals(productId) is not true) ProductId = productId;
        if (userEmail is not null && UserEmail?.Equals(userEmail) is not true) UserEmail = userEmail;
        return this;
    }
}
