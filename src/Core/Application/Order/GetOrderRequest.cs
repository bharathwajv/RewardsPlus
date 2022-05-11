namespace RewardsPlus.Application.Order;

public class GetOrderRequest : IRequest<OrderDto>
{
    public Guid Id { get; set; }
    public GetOrderRequest(Guid id) => Id = id;
}

public class OrderByIdSpec : Specification<Domain.OrderDomain.Order, OrderDto>, ISingleResultSpecification
{
    public OrderByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetOrderRequestHandler : IRequestHandler<GetOrderRequest, OrderDto>
{
    private readonly IReadRepository<Domain.OrderDomain.Order> _repository;
    private readonly IStringLocalizer<GetOrderRequestHandler> _localizer;

    public GetOrderRequestHandler(IReadRepository<Domain.OrderDomain.Order> repository, IStringLocalizer<GetOrderRequestHandler> localizer) => (_repository, _localizer) = (repository, localizer);

    public async Task<OrderDto> Handle(GetOrderRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Domain.OrderDomain.Order, OrderDto>)new OrderByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["Order.notfound"], request.Id));
}