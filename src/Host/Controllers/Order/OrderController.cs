using RewardsPlus.Application.Order;

namespace RewardsPlus.Host.Controllers.Multitenancy;

public class OrderController : VersionedApiController
{
    [HttpPost("place-order")]
    [MustHavePermission(FSHAction.UseService, FSHResource.Order)]
    [OpenApiOperation("Use Order Service .", "")]
    public Task<string> OrderProductAsync(BuyProductRequest request, CancellationToken cancellationToken)
    {
        return Mediator.Send(request, cancellationToken);
    }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Order)]
    [OpenApiOperation("Search orders using available filters.", "")]
    public Task<PaginationResponse<OrderDto>> SearchAsync(SearchOrdersRequest request, CancellationToken cancellationToken)
    {
        return Mediator.Send(request, cancellationToken);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Order)]
    [OpenApiOperation("Get order details.", "")]
    public Task<OrderDto> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return Mediator.Send(new GetOrderRequest(id), cancellationToken);
    }

    [HttpPut("update-status")]
    [MustHavePermission(FSHAction.Search, FSHResource.Order)]
    [OpenApiOperation("Update orders only by super admin token.", "")]
    public async Task<ActionResult<string>> UpdateStatusAsync(UpdateOrderRequest request, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(request), cancellationToken);
    }

    //deliver
    //Hangfire job 
}
