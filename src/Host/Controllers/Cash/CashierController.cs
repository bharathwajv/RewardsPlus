using RewardsPlus.Application.Cash;

namespace RewardsPlus.Host.Controllers.Multitenancy;

public class CashierController : VersionedApiController
{
    [HttpGet("getallbalance")]
    [MustHavePermission(FSHAction.View, FSHResource.Cashier)]
    [OpenApiOperation("Search tokens.", "")]
    public Task<List<CashDto>> GetListAsync(CancellationToken cancellationToken)
    {
        return Mediator.Send(new GetAllCashRequest(), cancellationToken);
    }

    [HttpPost("gift")]
    [MustHavePermission(FSHAction.GiftCash, FSHResource.Cashier)]
    [OpenApiOperation("GiftToken to other users.", "")]
    public Task<string> GiftCashAsync(GiftCashRequest request, CancellationToken cancellationToken)
    {
        return Mediator.Send(request, cancellationToken);
    }

    [HttpGet("balance")]
    [OpenApiOperation("View current balance", "")]
    public Task<double> GetCurrentBalance(CancellationToken cancellationToken)
    {
        return Mediator.Send(new GetCashBalanceRequest(), cancellationToken);
    }

    [HttpPost("buy")]
    [MustHavePermission(FSHAction.GiftCash, FSHResource.Cashier)]
    [OpenApiOperation("GiftToken to other users.", "")]
    public Task<string> BuyCashAsync(BuyCashRequest request, CancellationToken cancellationToken)
    {
        return Mediator.Send(request, cancellationToken);
    }

    //[HttpGet("{id}")]
    //[MustHavePermission(FSHAction.View, FSHResource.Tokens)]
    //[OpenApiOperation("View tokens by user id.", "")]
    //public Task<TokenDto> GetAsync(string id)
    //{
    //    return Mediator.Send(new GetTenantRequest(id));
    //}
}