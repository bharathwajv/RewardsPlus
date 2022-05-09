namespace RewardsPlus.Application.Order;

public class BuyProductRequest : IRequest<string>
{
    public Guid ProductName { get; set; }
}

public class BuyProductRequestHandler : IRequestHandler<BuyProductRequest, string>
{
    private readonly IButlerService _butlerService;

    public BuyProductRequestHandler(IButlerService butlerService) => _butlerService = butlerService;

    public Task<string> Handle(BuyProductRequest request, CancellationToken cancellationToken) =>
        _butlerService.PlaceOrder(request, cancellationToken);
}
