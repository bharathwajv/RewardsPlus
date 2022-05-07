namespace RewardsPlus.Application.Token;

public class GiftCashRequest : IRequest<string>
{
    public string ToEmailId { get; set; }
    public double Amount { get; set; }
}

public class GiftCashRequestHandler : IRequestHandler<GiftCashRequest, string>
{
    private readonly ICashierService _cashierService;

    public GiftCashRequestHandler(ICashierService cashierService) => _cashierService = cashierService;

    public Task<string> Handle(GiftCashRequest request, CancellationToken cancellationToken) =>
        _cashierService.GiftAsync(request, cancellationToken);
}