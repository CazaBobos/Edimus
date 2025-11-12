using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Core.Entities;

namespace Shared.Infrastructure.EntityConfiguration;
public class LayoutConfiguration : IEntityTypeConfiguration<Layout>
{
    public void Configure(EntityTypeBuilder<Layout> entity)
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id)
            .HasColumnName("LayoutId")
            .ValueGeneratedOnAdd();

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(16);

        entity.Property(e => e.EstablishmentId)
            .IsRequired();
        entity.HasOne(e => e.Establishment)
            .WithMany(e => e.Layouts)
            .HasForeignKey(e => e.EstablishmentId);

        entity.HasMany(e => e.Tables)
            .WithOne(e => e.Layout)
            .HasForeignKey(e => e.LayoutId);

        entity.HasMany(e => e.Sectors)
            .WithOne(e => e.Layout)
            .HasForeignKey(e => e.LayoutId);

        entity.HasMany(e => e.Walls)
            .WithOne(e => e.Layout)
            .HasForeignKey(e => e.LayoutId);

        entity.Property(e => e.Enabled)
            .IsRequired();
    }
}

