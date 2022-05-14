using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RewardsPlus.Application.Common.Events;
using RewardsPlus.Application.Common.Interfaces;
using RewardsPlus.Domain.CashDomain;
using RewardsPlus.Domain.Catalog;
using RewardsPlus.Domain.OrderDomain;

namespace RewardsPlus.Infrastructure.Persistence.Context;

public class ApplicationDbContext : BaseDbContext
{
    public ApplicationDbContext(ITenantInfo currentTenant, DbContextOptions options, ICurrentUser currentUser, ISerializerService serializer, IOptions<DatabaseSettings> dbSettings, IEventPublisher events)
        : base(currentTenant, options, currentUser, serializer, dbSettings, events)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<Cash> Cash => Set<Cash>(); // SchemaName defined in TokenConfig
    public DbSet<Order> Order => Set<Order>();
    public DbSet<GiftingInfo> GiftingInfo => Set<GiftingInfo>();

    // sales delivery
}