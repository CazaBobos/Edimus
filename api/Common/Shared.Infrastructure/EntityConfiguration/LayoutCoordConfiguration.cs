using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Core.Entities;

namespace Shared.Infrastructure.EntityConfiguration;
public class LayoutCoordConfiguration : IEntityTypeConfiguration<LayoutCoord>
{
    public void Configure(EntityTypeBuilder<LayoutCoord> entity)
    {
        entity.HasKey(e => new { e.X, e.Y, e.LayoutId });

        entity.Property(e => e.LayoutId)
            .IsRequired();
        entity.HasOne(e => e.Layout)
            .WithMany(e => e.Boundaries)
            .HasForeignKey(e => e.LayoutId);
    }
}

