namespace RewardsPlus.Domain.OrderDomain;

public class Order : AuditableEntity, IAggregateRoot
{
    public Guid ProductId { get; private set; }
    public string UserEmail { get; private set; }
    public OrderStatus Status { get; private set; }
    public string StatusMessage
    {
        get { return Status.ToString(); }
    }

    public Order(Guid productId, string userEmail, OrderStatus status)
    {
        ProductId = productId;
        UserEmail = userEmail;
        Status = status;
    }

    public Order Update(Guid productId, string userEmail, OrderStatus status)
    {
        if (productId != new Guid() && ProductId.Equals(productId) is not true) ProductId = productId;
        if (userEmail is not null && UserEmail?.Equals(userEmail) is not true) UserEmail = userEmail;
        if (!Status.Equals(status)) Status = status;
        return this;
    }
}

public enum OrderStatus
{
    Ordered,
    Delivered,
    OutOfStock
}
