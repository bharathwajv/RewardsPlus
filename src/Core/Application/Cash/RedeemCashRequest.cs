namespace RewardsPlus.Application.Cash;

// will be used from  buy Items service
public class RedeemCashRequest : IRequest<string>
{
    public RedeemCashRequest(decimal amount)
    {
        this.Amount = amount;
    }

    public decimal Amount { get; set; }
}

public class RedeemCashRequestHandler : IRequestHandler<RedeemCashRequest, string>
{
    private readonly ICashierService _cashierService;

    public RedeemCashRequestHandler(ICashierService cashierService) => _cashierService = cashierService;

    public Task<string> Handle(RedeemCashRequest request, CancellationToken cancellationToken) =>
        _cashierService.RedeemAsync(request, cancellationToken);
}