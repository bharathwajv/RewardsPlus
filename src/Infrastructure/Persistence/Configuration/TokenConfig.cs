using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RewardsPlus.Domain.Catalog; // todo - move to infrastructure and not domain

namespace RewardsPlus.Infrastructure.Persistence.Configuration;

public class TokenConfig : IEntityTypeConfiguration<Token>
{
    public void Configure(EntityTypeBuilder<Token> builder) =>
        builder
            .ToTable("Tokens", SchemaNames.Application)
            .IsMultiTenant();
}