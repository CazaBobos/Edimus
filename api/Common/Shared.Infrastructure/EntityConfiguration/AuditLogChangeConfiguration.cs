using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Core.Entities;

namespace Shared.Infrastructure.EntityConfiguration;

public class AuditLogChangeConfiguration : IEntityTypeConfiguration<AuditLogChange>
{
    public void Configure(EntityTypeBuilder<AuditLogChange> entity)
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).ValueGeneratedOnAdd();

        entity.Property(e => e.AuditLogId).IsRequired();
        entity.Property(e => e.PropertyName).IsRequired().HasMaxLength(64);
        entity.Property(e => e.OldValue).HasMaxLength(512);
        entity.Property(e => e.NewValue).HasMaxLength(512);
    }
}
