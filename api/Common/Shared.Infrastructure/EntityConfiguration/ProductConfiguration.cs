using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Core.Entities;

namespace Shared.Infrastructure.EntityConfiguration;
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> entity)
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id)
            .HasColumnName("ProductId")
            .ValueGeneratedOnAdd();

        entity.HasOne(e => e.Category)
            .WithMany(e => e.Products)
            .HasForeignKey(e => e.CategoryId);

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        entity.Property(e => e.Description)
            .HasMaxLength(250)
            .IsRequired();

        entity.HasOne(e => e.Image)
            .WithOne(e => e.Product)
            .HasForeignKey<Image>(e => e.ProductId);

        entity.HasMany(e => e.Tags)
            .WithMany(e => e.Products);

        entity.Property(e => e.Enabled)
            .IsRequired();
    }
}

