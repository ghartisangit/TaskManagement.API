using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Infrastructure.Identity;

namespace TaskManagement.Infrastructure.Configurations;

public class AppUserConfiguration:IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(u => u.UserName)
            .HasMaxLength(100);

        builder.Property(u => u.Email)
            .HasMaxLength(150);
    }
}
