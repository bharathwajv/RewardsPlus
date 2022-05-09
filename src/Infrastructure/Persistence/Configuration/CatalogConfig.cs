using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RewardsPlus.Domain.Catalog;

namespace RewardsPlus.Infrastructure.Persistence.Configuration;

public class BrandConfig : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder
            .ToTable(TableNames.Brand, SchemaNames.Catalog)
            .IsMultiTenant();

        builder
            .Property(b => b.Name)
                .HasMaxLength(256);
    }
}

public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .ToTable(TableNames.Product, SchemaNames.Catalog)
            .IsMultiTenant();

        builder
            .Property(b => b.Name)
                .HasMaxLength(1024);

        builder
            .Property(p => p.ImagePath)
                .HasMaxLength(2048);
    }
}