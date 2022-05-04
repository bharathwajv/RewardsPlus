namespace RewardsPlus.Application.Token;

public class GetAllTokensRequest : IRequest<List<TokenDto>>
{
}

public class GetAllTokensRequestHandler : IRequestHandler<GetAllTokensRequest, List<TokenDto>>
{
    private readonly ICashierService _tokenService;

    public GetAllTokensRequestHandler(ICashierService tokenService) => _tokenService = tokenService;

    public async Task<List<TokenDto>> Handle(GetAllTokensRequest request, CancellationToken cancellationToken) =>
                                                        await _tokenService.GetAllAsync();

}