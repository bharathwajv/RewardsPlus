using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using RewardsPlus.Infrastructure.Common.Services;

namespace Infrastructure.Test.Caching;

public class DistributedCacheService : CacheService<RewardsPlus.Infrastructure.Caching.DistributedCacheService>
{
    protected override RewardsPlus.Infrastructure.Caching.DistributedCacheService CreateCacheService() =>
        new(
            new MemoryDistributedCache(Options.Create(new MemoryDistributedCacheOptions())),
            new NewtonSoftService(),
            NullLogger<RewardsPlus.Infrastructure.Caching.DistributedCacheService>.Instance);
}