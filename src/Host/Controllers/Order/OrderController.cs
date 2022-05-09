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

    //deliver
    //Hangfire job 
}
