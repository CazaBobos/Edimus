using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Core.Entities;
using Shared.Core.Entities;

namespace Shared.Infrastructure.EntityConfiguration;
public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
{
    public void Configure(EntityTypeBuilder<Ingredient> entity)
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id)
            .HasColumnName("IngredientId")
            .ValueGeneratedOnAdd();

        entity.Property(e => e.Name)
            .IsRequired();
        entity.Property(e => e.Stock)
            .IsRequired();
        entity.Property(e => e.Alert)
            .IsRequired();
        entity.Property(e => e.Unit)
            .IsRequired();
        entity.Property(e => e.Enabled)
            .IsRequired();
    }
}

