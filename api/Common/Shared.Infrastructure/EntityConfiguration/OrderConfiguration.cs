using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Core.Entities;

namespace Shared.Infrastructure.EntityConfiguration;
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> entity)
    {
        entity.HasKey(e => new { e.TableId, e.ProductId });

        entity.Property(e => e.ProductId)
            .IsRequired();

        entity.Property(e => e.TableId)
            .IsRequired();

        entity.Property(e => e.Amount)
            .IsRequired();
    }
}

