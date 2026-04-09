using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Core.Entities;

namespace Shared.Infrastructure.EntityConfiguration;

public class TableSessionConfiguration : IEntityTypeConfiguration<TableSession>
{
    public void Configure(EntityTypeBuilder<TableSession> entity)
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).ValueGeneratedOnAdd();

        entity.Property(e => e.TableId).IsRequired();
        entity.HasOne(e => e.Table)
            .WithMany()
            .HasForeignKey(e => e.TableId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.Property(e => e.OpenedAt).IsRequired();
        entity.Property(e => e.ClosedAt);
        entity.Property(e => e.ArrivedAt);
        entity.Property(e => e.ArrivalAttentionSeconds);
        entity.Property(e => e.TotalCallingSeconds).IsRequired();
        entity.Property(e => e.CallingCount).IsRequired();

        entity.HasMany(e => e.Orders)
            .WithOne(e => e.Session)
            .HasForeignKey(e => e.SessionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
