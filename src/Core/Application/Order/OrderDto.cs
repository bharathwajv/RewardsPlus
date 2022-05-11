using RewardsPlus.Domain.OrderDomain;

namespace RewardsPlus.Application.Order;

public class OrderDto : IDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; private set; }
    public string UserEmail { get; private set; }
    public OrderStatus Status { get; private set; }
    public string StatusMessage
    {
        get { return this.Status.ToString(); }
    }
}