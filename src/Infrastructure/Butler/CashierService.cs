using MediatR;
using Microsoft.Extensions.Logging;
using RewardsPlus.Application.Common.Interfaces;
using RewardsPlus.Application.Common.Persistence;
using RewardsPlus.Application.Token;
using RewardsPlus.Domain.Catalog;

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
        _currentUser.GetUserEmail();
        Product product = await _repository.GetByIdAsync<string>((request?.ProductName), cancellationToken);
        if (product == null)
            throw new Exception("Product not found");

        if (product.Quantity > 0)
        {
            //product.Update(quantity)
        }
        //return await _mediator.Send(new BuyProductCommand(request.ProductName, request.Quantity), cancellationToken);

        return "Order placed successfully";
    }
}
//public class SelectedProductSpec : Specification<Product>
//{
//    public SelectedProductSpec(string id) =>
//        Query.Where(b => !string.IsNullOrEmpty(b.Name) && b.Id.Equals(id));
//}