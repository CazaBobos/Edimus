using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Core.Entities;

namespace Shared.Infrastructure.EntityConfiguration;
public class ConsumptionConfiguration : IEntityTypeConfiguration<Consumption>
{
    public void Configure(EntityTypeBuilder<Consumption> entity)
    {
        entity.HasKey(e => new { e.ProductId, e.IngredientId });

        entity.Property(e => e.ProductId)
            .IsRequired();
        entity.HasOne(e => e.Product)
            .WithMany(e => e.Consumptions)
            .HasForeignKey(e => e.ProductId);

        entity.Property(e => e.IngredientId)
            .IsRequired();
        entity.HasOne(e => e.Ingredient)
            .WithMany(e => e.Consumptions)
            .HasForeignKey(e => e.IngredientId);

        entity.Property(e => e.Amount)
            .IsRequired();
    }
}

