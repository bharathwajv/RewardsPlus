using RewardsPlus.Application.Order;

namespace RewardsPlus.Host.Controllers.Multitenancy;

public class OrderController : VersionedApiController
{
    [HttpPost("place-order")]
    [MustHavePermission(FSHAction.UseService, FSHResource.Order)]
    [OpenApiOperation("Use Order Service .", "")]
    public Task<string> OrderProductAsync(BuyProductRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Order)]
    [OpenApiOperation("Search orders using available filters.", "")]
    public Task<PaginationResponse<OrderDto>> SearchAsync(SearchOrdersRequest request)
    {
        return Mediator.Send(request);
    }

    //order by id specification


    //deliver
    //Hangfire job 
}
