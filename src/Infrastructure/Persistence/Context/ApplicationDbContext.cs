using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RewardsPlus.Application.Common.Events;
using RewardsPlus.Application.Common.Interfaces;
using RewardsPlus.Domain.Catalog;
using RewardsPlus.Infrastructure.Persistence.Configuration;

namespace RewardsPlus.Infrastructure.Persistence.Context;

public class ApplicationDbContext : BaseDbContext
{
    public ApplicationDbContext(ITenantInfo currentTenant, DbContextOptions options, ICurrentUser currentUser, ISerializerService serializer, IOptions<DatabaseSettings> dbSettings, IEventPublisher events)
        : base(currentTenant, options, currentUser, serializer, dbSettings, events)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<Token> Tokens => Set<Token>(); // SchemaName defined in TokenConfig

    // purchase coins

    // sales - purchase with coins

    // send coupons or purchased items

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(SchemaNames.Catalog);
    }
}