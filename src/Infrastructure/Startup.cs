using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RewardsPlus.Infrastructure.Auth;
using RewardsPlus.Infrastructure.BackgroundJobs;
using RewardsPlus.Infrastructure.Caching;
using RewardsPlus.Infrastructure.Common;
using RewardsPlus.Infrastructure.Cors;
using RewardsPlus.Infrastructure.FileStorage;
using RewardsPlus.Infrastructure.Localization;
using RewardsPlus.Infrastructure.Mailing;
using RewardsPlus.Infrastructure.Mapping;
using RewardsPlus.Infrastructure.Middleware;
using RewardsPlus.Infrastructure.Multitenancy;
using RewardsPlus.Infrastructure.Notifications;
using RewardsPlus.Infrastructure.OpenApi;
using RewardsPlus.Infrastructure.Persistence;
using RewardsPlus.Infrastructure.Persistence.Initialization;
using RewardsPlus.Infrastructure.SecurityHeaders;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Infrastructure.Test")]

namespace RewardsPlus.Infrastructure;

public static class Startup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        MapsterSettings.Configure();
        return services
            .AddApiVersioning()
            .AddAuth(config)
            .AddBackgroundJobs(config)
            .AddCaching(config)
            .AddCorsPolicy(config)
            .AddExceptionMiddleware()
            .AddHealthCheck()
            .AddPOLocalization(config)
            .AddMailing(config)
            .AddMediatR(Assembly.GetExecutingAssembly())
            .AddMultitenancy(config)
            .AddNotifications(config)
            .AddOpenApiDocumentation(config)
            .AddPersistence(config)
            .AddRequestLogging(config)
            .AddRouting(options => options.LowercaseUrls = true)
            .AddServices();
    }

    private static IServiceCollection AddApiVersioning(this IServiceCollection services) =>
        services.AddApiVersioning(config =>
        {
            config.DefaultApiVersion = new ApiVersion(1, 0);
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.ReportApiVersions = true;
        });

    private static IServiceCollection AddHealthCheck(this IServiceCollection services) =>
        services.AddHealthChecks().AddCheck<TenantHealthCheck>("Tenant").Services;

    public static async Task InitializeDatabasesAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
    {
        // Create a new scope to retrieve scoped services
        using var scope = services.CreateScope();

        await scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>()
            .InitializeDatabasesAsync(cancellationToken);
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder, IConfiguration config) =>
        builder
            .UseRequestLocalization()
            .UseStaticFiles()
            .UseSecurityHeaders(config)
            .UseFileStorage()
            .UseExceptionMiddleware()
            .UseRouting()
            .UseCorsPolicy()
            .UseAuthentication()
            .UseCurrentUser()
            .UseMultiTenancy()
            .UseAuthorization()
            .UseRequestLogging(config)
            .UseHangfireDashboard(config)
            .UseOpenApiDocumentation(config);

    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapControllers().RequireAuthorization();
        builder.MapHealthCheck();
        builder.MapNotifications();
        return builder;
    }

    private static IEndpointConventionBuilder MapHealthCheck(this IEndpointRouteBuilder endpoints) =>
        endpoints.MapHealthChecks("/api/health").RequireAuthorization();
}