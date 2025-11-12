using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Core.Entities;

namespace Shared.Infrastructure.EntityConfiguration;
public class SectorCoordConfiguration : IEntityTypeConfiguration<SectorCoord>
{
    public void Configure(EntityTypeBuilder<SectorCoord> entity)
    {
        entity.HasKey(e => new { e.X, e.Y, e.SectorId });

        entity.Property(e => e.SectorId)
            .IsRequired();
        entity.HasOne(e => e.Sector)
            .WithMany(e => e.Surface)
            .HasForeignKey(e => e.SectorId);
    }
}

