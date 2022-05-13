using RewardsPlus.Application.Token;

namespace RewardsPlus.Host.Controllers.Multitenancy;

public class TenantsController : VersionNeutralApiController
{
    [HttpGet]
    [MustHavePermission(FSHAction.View, FSHResource.Tenants)]
    [OpenApiOperation("Get a list of all tenants.", "")]
    public Task<List<TenantDto>> GetListAsync(CancellationToken cancellationToken)
    {
        return Mediator.Send(new GetAllTenantsRequest(), cancellationToken);
    }

    [HttpGet("{id}")]
    [MustHavePermission(FSHAction.View, FSHResource.Tenants)]
    [OpenApiOperation("Get tenant details.", "")]
    public Task<TenantDto> GetAsync(string id, CancellationToken cancellationToken)
    {
        return Mediator.Send(new GetTenantRequest(id), cancellationToken);
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Tenants)]
    [OpenApiOperation("Create a new tenant.", "")]
    public Task<string> CreateAsync(CreateTenantRequest request, CancellationToken cancellationToken)
    {
        return Mediator.Send(request, cancellationToken);
    }

    [HttpPost("{id}/activate")]
    [MustHavePermission(FSHAction.Update, FSHResource.Tenants)]
    [OpenApiOperation("Activate a tenant.", "")]
    [ApiConventionMethod(typeof(RPApiConventions), nameof(RPApiConventions.Register))]
    public Task<string> ActivateAsync(string id, CancellationToken cancellationToken)
    {
        return Mediator.Send(new ActivateTenantRequest(id), cancellationToken);
    }

    [HttpPost("{id}/deactivate")]
    [MustHavePermission(FSHAction.Update, FSHResource.Tenants)]
    [OpenApiOperation("Deactivate a tenant.", "")]
    [ApiConventionMethod(typeof(RPApiConventions), nameof(RPApiConventions.Register))]
    public Task<string> DeactivateAsync(string id, CancellationToken cancellationToken)
    {
        return Mediator.Send(new DeactivateTenantRequest(id), cancellationToken);
    }

    [HttpPost("{id}/upgrade")]
    [MustHavePermission(FSHAction.UpgradeSubscription, FSHResource.Tenants)]
    [OpenApiOperation("Upgrade a tenant's subscription.", "")]
    [ApiConventionMethod(typeof(RPApiConventions), nameof(RPApiConventions.Register))]
    public async Task<ActionResult<string>> UpgradeSubscriptionAsync(string id, UpgradeSubscriptionRequest request, CancellationToken cancellationToken)
    {
        return id != request.TenantId
            ? BadRequest()
            : Ok(await Mediator.Send(request, cancellationToken));
    }
}