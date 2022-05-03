namespace RewardsPlus.Application.Token;

public class SearchTokensRequest : PaginationFilter, IRequest<PaginationResponse<TokenDto>>
{
}

public class TokenBySearchRequestSpec : EntitiesByPaginationFilterSpec<Domain.Catalog.Token, TokenDto>
{
    public TokenBySearchRequestSpec(SearchTokensRequest request)
        : base(request) =>
        Query.OrderBy(c => c.UserEmail, !request.HasOrderBy());
}

public class SearchTokensRequestHandler : IRequestHandler<SearchTokensRequest, PaginationResponse<TokenDto>>
{
    private readonly IReadRepository<Domain.Catalog.Token> _repository;
    public SearchTokensRequestHandler(IReadRepository<Domain.Catalog.Token> repository) => _repository = repository;

    public async Task<PaginationResponse<TokenDto>> Handle(SearchTokensRequest request, CancellationToken cancellationToken)
    {
        var spec = new TokenBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}