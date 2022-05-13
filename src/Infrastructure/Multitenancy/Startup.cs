using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using RewardsPlus.Application.Token;
using RewardsPlus.Infrastructure.Persistence;
using RewardsPlus.Shared.Authorization;
using RewardsPlus.Shared.Multitenancy;
using Microsoft.Extensions.Options;

namespace RewardsPlus.Infrastructure.Multitenancy;

internal static class Startup
{
    internal static IServiceCollection AddMultitenancy(this IServiceCollection services, IConfiguration config)
    {
         return services
            .AddDbContext<TenantDbContext>((p, m) =>
            {
                // TODO: We should probably add specific dbprovider/connectionstring setting for the tenantDb with a fallback to the main databasesettings
                var databaseSettings = p.GetRequiredService<IOptions<DatabaseSettings>>().Value;
                m.UseDatabase(databaseSettings.DBProvider, databaseSettings.ConnectionString);
            })
            .AddMultiTenant<FSHTenantInfo>()
                .WithClaimStrategy(FSHClaims.Tenant)
                .WithHeaderStrategy(MultitenancyConstants.TenantIdName)
                .WithQueryStringStrategy(MultitenancyConstants.TenantIdName)
                .WithEFCoreStore<TenantDbContext, FSHTenantInfo>()
                .Services
            .AddScoped<ITenantService, TenantService>();
    }

    internal static IApplicationBuilder UseMultiTenancy(this IApplicationBuilder app) =>
        app.UseMultiTenant();

    private static FinbuckleMultiTenantBuilder<FSHTenantInfo> WithQueryStringStrategy(this FinbuckleMultiTenantBuilder<FSHTenantInfo> builder, string queryStringKey) =>
        builder.WithDelegateStrategy(context =>
        {
            if (context is not HttpContext httpContext)
            {
                return Task.FromResult((string?)null);
            }

            httpContext.Request.Query.TryGetValue(queryStringKey, out StringValues tenantIdParam);

            return Task.FromResult((string?)tenantIdParam.ToString());
        });
}