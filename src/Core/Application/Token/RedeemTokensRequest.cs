namespace RewardsPlus.Application.Token;

public class RedeemTokensRequest : IRequest<string>
{
    public double Amount { get; set; }
}

public class RedeemTokensRequestHandler : IRequestHandler<RedeemTokensRequest, string>
{
    private readonly ICashierService _tokenService;

    public RedeemTokensRequestHandler(ICashierService tokenService) => _tokenService = tokenService;

    public Task<string> Handle(RedeemTokensRequest request, CancellationToken cancellationToken) =>
        _tokenService.RedeemAsync(request, cancellationToken);
}