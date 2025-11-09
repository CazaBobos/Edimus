using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Core.Entities;

namespace Shared.Infrastructure.EntityConfiguration;
public class TableConfiguration : IEntityTypeConfiguration<Table>
{
    public void Configure(EntityTypeBuilder<Table> entity)
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id)
            .HasColumnName("TableId")
            .ValueGeneratedOnAdd();

        entity.Property(e => e.LayoutId)
            .IsRequired();
        entity.HasOne(e => e.Layout)
            .WithMany(e => e.Tables)
            .HasForeignKey(e => e.LayoutId);

        entity.Property(e => e.Status)
            .IsRequired();
        entity.Property(e => e.QR)
            .IsRequired();
        entity.Property(e => e.PositionX)
            .IsRequired();
        entity.Property(e => e.PositionY)
            .IsRequired();

        entity.Property(e => e.Surface)
            .IsRequired();

        entity.HasMany(e => e.Requests)
            .WithOne(e => e.Table)
            .HasForeignKey(e => e.TableId);

        entity.Property(e => e.Enabled)
            .IsRequired();
    }
}

