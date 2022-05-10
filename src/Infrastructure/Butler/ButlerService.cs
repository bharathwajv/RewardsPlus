using MediatR;
using Microsoft.Extensions.Logging;
using RewardsPlus.Application.Cash;
using RewardsPlus.Application.Common.Interfaces;
using RewardsPlus.Application.Common.Persistence;
using RewardsPlus.Application.Order;
using RewardsPlus.Domain.Catalog;
using RewardsPlus.Domain.OrderDomain;

namespace RewardsPlus.Infrastructure.Multitenancy;

internal class ButlerService : IButlerService
{
    private readonly ICurrentUser _currentUser;
    private readonly ILogger<ButlerService> _logger;
    private readonly ISender _mediator;
    private readonly IReadRepository<Product> _repository;
    public ButlerService(ICurrentUser currentUser, ILogger<ButlerService> logger, ISender mediator, IReadRepository<Product> repository) => (_currentUser, _logger, _mediator, _repository) = (currentUser, logger, mediator, repository);

    public async Task<string> PlaceOrder(BuyProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.ProductId);

        await _mediator.Send(new RedeemCashRequest(product.Rate), cancellationToken);
        await _mediator.Send(new CreateOrderRequest(request.ProductId, OrderStatus.Ordered), cancellationToken);

        //move validations to validator
        //if (product.Quantity > 0)
        //{
        //    //product.Update(quantity)
        //}
        //return await _mediator.Send(new BuyProductCommand(request.ProductName, request.Quantity), cancellationToken);

        return "Order placed successfully";
    }
}
//public class SelectedProductSpec : Specification<Product>
//{
//    public SelectedProductSpec(string id) =>
//        Query.Where(b => !string.IsNullOrEmpty(b.Name) && b.Id.Equals(id));
//}