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

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Order)]
    [OpenApiOperation("Get order details.", "")]
    public Task<OrderDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetOrderRequest(id));
    }

    [HttpPut("update-status")]
    [MustHavePermission(FSHAction.Search, FSHResource.Order)]
    [OpenApiOperation("Update orders only by super admin token.", "")]
    public async Task<ActionResult<string>> UpdateStatusAsync(UpdateOrderRequest request)
    {
        return Ok(await Mediator.Send(request));
    }

    //deliver
    //Hangfire job 
}
