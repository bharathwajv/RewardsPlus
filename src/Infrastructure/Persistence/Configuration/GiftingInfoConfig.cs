using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RewardsPlus.Domain.CashDomain; // AskExperts - move to infrastructure and not domain

namespace RewardsPlus.Infrastructure.Persistence.Configuration;

public class GiftingInfoConfig : IEntityTypeConfiguration<GiftingInfo>
{
    public void Configure(EntityTypeBuilder<GiftingInfo> builder) =>
        builder
            .ToTable(TableNames.GiftingInfo, SchemaNames.Application)
            .IsMultiTenant();
}