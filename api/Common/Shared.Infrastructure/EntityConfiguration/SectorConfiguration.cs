using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Core.Entities;

namespace Shared.Infrastructure.EntityConfiguration;
public class SectorConfiguration : IEntityTypeConfiguration<Sector>
{
    public void Configure(EntityTypeBuilder<Sector> entity)
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id)
            .HasColumnName("SectorId")
            .ValueGeneratedOnAdd();

        entity.Property(e => e.LayoutId)
            .IsRequired();
        entity.HasOne(e => e.Layout)
            .WithMany(e => e.Sectors)
            .HasForeignKey(e => e.LayoutId);

        entity.Property(e => e.Name)
            .IsRequired();
        entity.Property(e => e.PositionX)
            .IsRequired();
        entity.Property(e => e.PositionY)
            .IsRequired();

        entity.HasMany(e => e.Surface)
            .WithOne(e => e.Sector)
            .HasForeignKey(e => e.SectorId);

        entity.Property(e => e.Enabled)
            .IsRequired();
    }
}

