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
    public Task<decimal> GetCurrentBalance(CancellationToken cancellationToken)
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

    [HttpPost("history")]
    [MustHavePermission(FSHAction.View, FSHResource.GiftMyInfo)]
    [OpenApiOperation("View list of gift info.", "")]
    public Task<PaginationResponse<GiftingInfoDto>> GetMyGiftingInfoAsync(CancellationToken cancellationToken)
    {
        return Mediator.Send(new SearchMyGiftingInfoRequest(), cancellationToken);
    }
}