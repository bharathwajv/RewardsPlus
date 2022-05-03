using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.Abstractions;

namespace Infrastructure.Test.Caching;

public class LocalCacheService : CacheService<RewardsPlus.Infrastructure.Caching.LocalCacheService>
{
    protected override RewardsPlus.Infrastructure.Caching.LocalCacheService CreateCacheService() =>
        new(
            new MemoryCache(new MemoryCacheOptions()),
            NullLogger<RewardsPlus.Infrastructure.Caching.LocalCacheService>.Instance);
}