namespace RewardsPlus.Application.Cash;

// temp class search Cash bd

// AskExperts can we use Domain.Catalog.Cash here
public class SearchCashRequest : PaginationFilter, IRequest<PaginationResponse<CashDto>>
{
}

public class TokenBySearchRequestSpec : EntitiesByPaginationFilterSpec<Domain.CashDomain.Cash, CashDto>
{
    public TokenBySearchRequestSpec(SearchCashRequest request)
        : base(request) =>
        Query.OrderBy(c => c.UserEmail, !request.HasOrderBy());
}

public class SearchTokensRequestHandler : IRequestHandler<SearchCashRequest, PaginationResponse<CashDto>>
{
    private readonly IReadRepository<Domain.CashDomain.Cash> _repository;
    public SearchTokensRequestHandler(IReadRepository<Domain.CashDomain.Cash> repository) => _repository = repository;

    public async Task<PaginationResponse<CashDto>> Handle(SearchCashRequest request, CancellationToken cancellationToken)
    {
        var spec = new TokenBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}