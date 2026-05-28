using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Configurations;

public class RefreshTokenConfiguration:IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Token)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(r => r.Token)
           .IsUnique();

        builder.Property(r => r.CreatedByIp)
            .IsRequired();

        builder.Property(r => r.ExpiresAt)
            .IsRequired();
    }
}
