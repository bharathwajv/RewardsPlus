namespace RewardsPlus.Application.Token;

public class GiftTokensRequest : IRequest<string>
{
    public string? ToEmailId { get; set; }
    public double Amount { get; set; }
}

public class GiftTokensRequestHandler : IRequestHandler<GiftTokensRequest, string>
{
    private readonly ITokenService _tokenService;

    public GiftTokensRequestHandler(ITokenService tokenService) => _tokenService = tokenService;

    public Task<string> Handle(GiftTokensRequest request, CancellationToken cancellationToken) =>
        _tokenService.GiftAsync(request, cancellationToken);
}