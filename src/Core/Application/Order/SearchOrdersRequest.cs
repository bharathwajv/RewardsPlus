namespace RewardsPlus.Application.Order;

public class SearchOrdersRequest : PaginationFilter, IRequest<PaginationResponse<OrderDto>>
{
}

public class SearchByOrdersRequestSpec : EntitiesByPaginationFilterSpec<Domain.OrderDomain.Order, OrderDto>
{
    public SearchByOrdersRequestSpec(SearchOrdersRequest request)
        : base(request) =>
        Query.OrderBy(c => c.UserEmail, !request.HasOrderBy());
}

public class SearchOrdersRequestHandler : IRequestHandler<SearchOrdersRequest, PaginationResponse<OrderDto>>
{
    private readonly IReadRepository<Domain.OrderDomain.Order> _repository;

    public SearchOrdersRequestHandler(IReadRepository<Domain.OrderDomain.Order> repository) => _repository = repository;

    public async Task<PaginationResponse<OrderDto>> Handle(SearchOrdersRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchByOrdersRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}