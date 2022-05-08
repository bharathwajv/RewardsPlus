namespace RewardsPlus.Application.Token;

public class BuyProductRequest : IRequest<string>
{
    public string ProductName { get; set; }
}

public class BuyProductRequestHandler : IRequestHandler<BuyProductRequest, string>
{
    private readonly IButlerService _butlerService;

    public BuyProductRequestHandler(IButlerService butlerService) => _butlerService = butlerService;

    public Task<string> Handle(BuyProductRequest request, CancellationToken cancellationToken) =>
        _butlerService.PlaceOrder(request, cancellationToken);
}
