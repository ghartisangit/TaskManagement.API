using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Configurations;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u=> u.Id);

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Role)
            .IsRequired()
            .HasConversion<string>();
       
    }
}
