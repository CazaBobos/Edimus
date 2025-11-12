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

        entity.Property(e => e.PremiseId)
            .IsRequired();
        entity.HasOne(e => e.Premise)
            .WithMany(e => e.Layouts)
            .HasForeignKey(e => e.PremiseId);

        entity.HasMany(e => e.Tables)
            .WithOne(e => e.Layout)
            .HasForeignKey(e => e.LayoutId);

        entity.HasMany(e => e.Sectors)
            .WithOne(e => e.Layout)
            .HasForeignKey(e => e.LayoutId);

        entity.HasMany(e => e.Boundaries)
            .WithOne(e => e.Layout)
            .HasForeignKey(e => e.LayoutId);

        entity.Property(e => e.Enabled)
            .IsRequired();
    }
}

