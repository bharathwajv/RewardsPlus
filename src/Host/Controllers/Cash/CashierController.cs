using RewardsPlus.Application.Cash;

namespace RewardsPlus.Host.Controllers.Multitenancy;

public class CashierController : VersionedApiController
{
    [HttpGet("getallbalance")]
    [MustHavePermission(FSHAction.View, FSHResource.Cashier)]
    [OpenApiOperation("Search tokens.", "")]
    public Task<List<CashDto>> GetListAsync()
    {
        return Mediator.Send(new GetAllCashRequest());
    }

    [HttpPost("gift")]
    [MustHavePermission(FSHAction.GiftCash, FSHResource.Cashier)]
    [OpenApiOperation("GiftToken to other users.", "")]
    public Task<string> GiftCashAsync(GiftCashRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("balance")]
    [OpenApiOperation("View current balance", "")]
    public Task<double> GetCurrentBalance()
    {
        return Mediator.Send(new GetCashBalanceRequest());
    }

    [HttpPost("buy")]
    [MustHavePermission(FSHAction.GiftCash, FSHResource.Cashier)]
    [OpenApiOperation("GiftToken to other users.", "")]
    public Task<string> BuyCashAsync(BuyCashRequest request)
    {
        return Mediator.Send(request);
    }

    //[HttpGet("{id}")]
    //[MustHavePermission(FSHAction.View, FSHResource.Tokens)]
    //[OpenApiOperation("View tokens by user id.", "")]
    //public Task<TokenDto> GetAsync(string id)
    //{
    //    return Mediator.Send(new GetTenantRequest(id));
    //}
}