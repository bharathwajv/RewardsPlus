using RewardsPlus.Domain.OrderDomain;

namespace RewardsPlus.Application.Order;

public class OrderDto : IDto
{
    public string ProductId { get; private set; }
    public string UserEmail { get; private set; }
    public OrderStatus Status { get; private set; }

}