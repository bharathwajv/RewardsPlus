using RewardsPlus.Application.Dashboard;

namespace RewardsPlus.Host.Controllers.Dashboard;

public class DashboardController : VersionedApiController
{
    [HttpGet]
    [MustHavePermission(FSHAction.View, FSHResource.Dashboard)]
    [OpenApiOperation("Get statistics for the dashboard.", "")]
    public Task<StatsDto> GetAsync(CancellationToken cancellationToken)
    {
        return Mediator.Send(new GetStatsRequest(), cancellationToken);
    }
}