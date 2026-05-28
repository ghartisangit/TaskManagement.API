using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Configurations;

public class TaskItemConfiguration: IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.HasKey(t=> t.Id);

        builder.Property(t=> t.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t=> t.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(t => t.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.HasOne(t => t.CreatedByManager)
           .WithMany(u => u.ManagedTask)
           .HasForeignKey(t => t.CreatedByManagerId)
           .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.AssignedToDeveloper)
            .WithMany(u => u.AssignedTask)
            .HasForeignKey(t => t.AssignedToDeveloperId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
