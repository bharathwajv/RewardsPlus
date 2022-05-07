﻿using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RewardsPlus.Domain.Catalog; // AskExperts - move to infrastructure and not domain

namespace RewardsPlus.Infrastructure.Persistence.Configuration;

public class CashConfig : IEntityTypeConfiguration<Cash>
{
    public void Configure(EntityTypeBuilder<Cash> builder) =>
        builder
            .ToTable("Cash", SchemaNames.Application)
            .IsMultiTenant();
}