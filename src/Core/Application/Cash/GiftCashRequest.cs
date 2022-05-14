namespace RewardsPlus.Application.Cash;

public class GiftCashRequest : IRequest<string>
{
    public string? FromUserEmail { get; set; }
    public string ToUserEmail { get; set; }
    public string GiftMessage { get; set; }
    public string? GiftImage { get; set; }
    public bool IsViewed { get; set; }
    public double Amount { get; set; }
}

public class GiftCashRequestHandler : IRequestHandler<GiftCashRequest, string>
{
    private readonly ICashierService _cashierService;

    public GiftCashRequestHandler(ICashierService cashierService) => _cashierService = cashierService;

    public Task<string> Handle(GiftCashRequest request, CancellationToken cancellationToken) =>
        _cashierService.GiftAsync(request, cancellationToken);
}