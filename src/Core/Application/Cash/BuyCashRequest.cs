namespace RewardsPlus.Application.Token;

public class BuyCashRequest : IRequest<string>
{
    public double Amount { get; set; }
}

public class BuyCashRequestHandler : IRequestHandler<BuyCashRequest, string>
{
    private readonly ICashierService _cashierService;

    public BuyCashRequestHandler(ICashierService tokenService) => _cashierService = tokenService;

    public Task<string> Handle(BuyCashRequest request, CancellationToken cancellationToken) =>
        _cashierService.BuyAsync(request, cancellationToken);
}