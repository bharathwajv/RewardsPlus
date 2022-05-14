using RewardsPlus.Domain.CashDomain;

namespace RewardsPlus.Application.Cash;

public class SearchMyGiftingInfoRequest : PaginationFilter, IRequest<PaginationResponse<GiftingInfoDto>>
{
}

public class SearchGiftingInfoRequestSpec : EntitiesByPaginationFilterSpec<GiftingInfo, GiftingInfoDto>
{
    public SearchGiftingInfoRequestSpec(SearchMyGiftingInfoRequest request, string currentUser)
        : base(request) =>
        Query.Where(c => c.ToUserEmail == currentUser);
}

public class SearchGiftingInfoRequestHandler : IRequestHandler<SearchMyGiftingInfoRequest, PaginationResponse<GiftingInfoDto>>
{
    private readonly IReadRepository<GiftingInfo> _repository;
    private readonly ICurrentUser _currentUser;

    public SearchGiftingInfoRequestHandler(IReadRepository<GiftingInfo> repository, ICurrentUser currentUser) => (_repository, _currentUser) = (repository, currentUser);

    public async Task<PaginationResponse<GiftingInfoDto>> Handle(SearchMyGiftingInfoRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchGiftingInfoRequestSpec(request, _currentUser?.GetUserEmail());
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}