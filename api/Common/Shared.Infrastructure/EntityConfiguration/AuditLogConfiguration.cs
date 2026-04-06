using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Core.Entities;

namespace Shared.Infrastructure.EntityConfiguration;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> entity)
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).ValueGeneratedOnAdd();

        entity.Property(e => e.EntityType).IsRequired().HasMaxLength(64);
        entity.Property(e => e.EntityId).IsRequired().HasMaxLength(32);
        entity.Property(e => e.Operation).IsRequired();
        entity.Property(e => e.UserId).IsRequired();
        entity.Property(e => e.Username).IsRequired().HasMaxLength(32);
        entity.Property(e => e.DateTime).IsRequired();

        entity.HasMany(e => e.Changes)
            .WithOne()
            .HasForeignKey(c => c.AuditLogId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
