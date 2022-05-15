using RewardsPlus.Domain.CashDomain;

namespace RewardsPlus.Application.Cash;

public class BuyCashRequest : IRequest<string>
{
    public BuyMode Mode { get; set; }
    public decimal Amount { get; set; }
}

public class BuyCashRequestHandler : IRequestHandler<BuyCashRequest, string>
{
    private readonly ICashierService _cashierService;

    public BuyCashRequestHandler(ICashierService cashierService) => _cashierService = cashierService;

    public Task<string> Handle(BuyCashRequest request, CancellationToken cancellationToken) =>
        _cashierService.BuyAsync(request, cancellationToken);
}