using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace RewardsPlus.Infrastructure.Multitenancy;

public class TenantHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        // Descoped
        var check = new HealthCheckResult(HealthStatus.Healthy);
        return Task.FromResult(check);
    }
}