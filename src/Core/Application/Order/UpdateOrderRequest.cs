using RewardsPlus.Domain.OrderDomain;

namespace RewardsPlus.Application.Order;

public class UpdateOrderRequest : IRequest<string>
{
    public Guid Id { get; set; }
    public OrderStatus Status { get; set; }
}

public class UpdateOrderRequestHandler : IRequestHandler<UpdateOrderRequest, string>
{
    private readonly IRepositoryWithEvents<Domain.OrderDomain.Order> _repository;
    private readonly IStringLocalizer<UpdateOrderRequestHandler> _localizer;

    public UpdateOrderRequestHandler(IRepositoryWithEvents<Domain.OrderDomain.Order> repository, IStringLocalizer<UpdateOrderRequestHandler> localizer) =>
                                            (_repository, _localizer) = (repository, localizer);

    public async Task<string> Handle(UpdateOrderRequest request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = order ?? throw new NotFoundException(string.Format(_localizer["Order.notfound"], request.Id));

        order.Update(request.Id, request.Status);

        await _repository.UpdateAsync(order, cancellationToken);

        return "Updated successfully";
    }
}