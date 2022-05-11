namespace RewardsPlus.Domain.OrderDomain;

public class Order : AuditableEntity, IAggregateRoot
{
    public Guid ProductId { get; private set; }
    public string UserEmail { get; private set; }
    public OrderStatus Status { get; private set; }
    public string StatusMessage { get; private set; }

    public Order(Guid productId, string userEmail, OrderStatus status)
    {
        ProductId = productId;
        UserEmail = userEmail;
        Status = status;
        StatusMessage = status.ToString();
    }

    public Order Update(Guid productId, OrderStatus status)
    {
        if (productId != new Guid() && ProductId.Equals(productId) is not true) ProductId = productId;
        if (!Status.Equals(status))
        {
            this.Status = status;
            this.StatusMessage = status.ToString();
        }

        return this;
    }
}
