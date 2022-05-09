using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RewardsPlus.Infrastructure.Auditing;

namespace RewardsPlus.Infrastructure.Persistence.Configuration;

public class AuditTrailConfig : IEntityTypeConfiguration<Trail>
{
    public void Configure(EntityTypeBuilder<Trail> builder) =>
        builder
            .ToTable(TableNames.AuditTrails, SchemaNames.Auditing)
            .IsMultiTenant();
}