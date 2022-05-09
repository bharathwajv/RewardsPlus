using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;// AskExperts - move to infrastructure and not domain
using RewardsPlus.Domain.Order;

namespace RewardsPlus.Infrastructure.Persistence.Configuration;

public class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder) =>
        builder
            .ToTable("Order", SchemaNames.Application)
            .IsMultiTenant();
}