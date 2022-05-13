using RewardsPlus.Application.Common.Interfaces;
using RewardsPlus.Application.Common.Persistence;
using RewardsPlus.Infrastructure.Caching;
using RewardsPlus.Infrastructure.Common.Services;
using RewardsPlus.Infrastructure.Localization;
using RewardsPlus.Infrastructure.Persistence.ConnectionString;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Test;

public class Startup
{
    public void ConfigureHost(IHostBuilder host) =>
        host.ConfigureHostConfiguration(config => config.AddJsonFile("appsettings.json"));

    public void ConfigureServices(IServiceCollection services, HostBuilderContext context) =>
        services
            .AddTransient<IMemoryCache, MemoryCache>()
            .AddTransient<LocalCacheService>()
            .AddTransient<IDistributedCache, MemoryDistributedCache>()
            .AddTransient<ISerializerService, NewtonSoftService>()
            .AddTransient<DistributedCacheService>()

            .AddPOLocalization(context.Configuration)

            .AddTransient<IConnectionStringSecurer, ConnectionStringSecurer>();
}