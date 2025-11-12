using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Core.Entities;

namespace Shared.Infrastructure.EntityConfiguration;
public class TableCoordConfiguration : IEntityTypeConfiguration<TableCoord>
{
    public void Configure(EntityTypeBuilder<TableCoord> entity)
    {
        entity.HasKey(e => new { e.X, e.Y, e.TableId });

        entity.Property(e => e.TableId)
            .IsRequired();
        entity.HasOne(e => e.Table)
            .WithMany(e => e.Surface)
            .HasForeignKey(e => e.TableId);
    }
}

