using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RewardsPlus.Infrastructure.Identity;

namespace RewardsPlus.Infrastructure.Persistence.Configuration;

public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder
            .ToTable(TableNames.User, SchemaNames.Identity)
            .IsMultiTenant();

        builder
            .Property(u => u.ObjectId)
                .HasMaxLength(256);
    }
}

public class ApplicationRoleConfig : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder) =>
        builder
            .ToTable(TableNames.Roles, SchemaNames.Identity)
            .IsMultiTenant()
                .AdjustUniqueIndexes();
}

public class ApplicationRoleClaimConfig : IEntityTypeConfiguration<ApplicationRoleClaim>
{
    public void Configure(EntityTypeBuilder<ApplicationRoleClaim> builder) =>
        builder
            .ToTable(TableNames.RoleClaims, SchemaNames.Identity)
            .IsMultiTenant();
}

public class IdentityUserRoleConfig : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder) =>
        builder
            .ToTable(TableNames.UserRoles, SchemaNames.Identity)
            .IsMultiTenant();
}

public class IdentityUserClaimConfig : IEntityTypeConfiguration<IdentityUserClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder) =>
        builder
            .ToTable(TableNames.UserClaims, SchemaNames.Identity)
            .IsMultiTenant();
}

public class IdentityUserLoginConfig : IEntityTypeConfiguration<IdentityUserLogin<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder) =>
        builder
            .ToTable(TableNames.UserLogins, SchemaNames.Identity)
            .IsMultiTenant();
}

public class IdentityUserTokenConfig : IEntityTypeConfiguration<IdentityUserToken<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder) =>
        builder
            .ToTable(TableNames.UserTokens, SchemaNames.Identity)
            .IsMultiTenant();
}