using RewardsPlus.Domain.OrderDomain;

namespace RewardsPlus.Application.Order;

public class CreateOrderRequest : IRequest<Guid>
{
    public Guid ProductId { get; private set; }
    public OrderStatus Status { get; private set; }
    public CreateOrderRequest(Guid productId, OrderStatus ordered)
    {
        this.ProductId = productId;
        this.Status = ordered;
    }
}

public class CreateOrderRequestHandler : IRequestHandler<CreateOrderRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Domain.OrderDomain.Order> _repository;
    private readonly ICurrentUser _currentUser;
    public CreateOrderRequestHandler(IRepositoryWithEvents<Domain.OrderDomain.Order> repository, ICurrentUser currentUser) => (_repository, _currentUser) = (repository, currentUser);

    public async Task<Guid> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var order = new Domain.OrderDomain.Order(request.ProductId, _currentUser?.GetUserEmail(), request.Status);

        await _repository.AddAsync(order, cancellationToken);

        return order.Id;
    }
}