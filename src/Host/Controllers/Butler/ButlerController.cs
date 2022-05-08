using RewardsPlus.Application.Token;

namespace RewardsPlus.Host.Controllers.Multitenancy;

public class ButlerController : VersionedApiController
{
    [HttpPost("buy")]
    [MustHavePermission(FSHAction.GiftCash, FSHResource.Butler)]
    [OpenApiOperation("GiftToken to other users.", "")]
    public Task<string> GiftCashAsync(BuyProductRequest request)
    {
        return Mediator.Send(request);
    }

    //deliver
    //Hangfire job 
}
