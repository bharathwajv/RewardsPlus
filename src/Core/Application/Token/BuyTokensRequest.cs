namespace RewardsPlus.Application.Token;

public class BuyTokensRequest : IRequest<string>
{
    public double Amount { get; set; }
}

public class BuyTokensRequestHandler : IRequestHandler<BuyTokensRequest, string>
{
    private readonly ICashierService _tokenService;

    public BuyTokensRequestHandler(ICashierService tokenService) => _tokenService = tokenService;

    public Task<string> Handle(BuyTokensRequest request, CancellationToken cancellationToken) =>
        _tokenService.BuyAsync(request, cancellationToken);
}