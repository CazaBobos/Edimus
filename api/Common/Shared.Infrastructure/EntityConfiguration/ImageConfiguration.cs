using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Core.Entities;

namespace Shared.Infrastructure.EntityConfiguration;
public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> entity)
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id)
            .HasColumnName("ImageId")
            .ValueGeneratedOnAdd();

        entity.Property(e => e.ProductId)
            .IsRequired();
        entity.HasOne(e => e.Product)
            .WithOne(e => e.Image)
            .HasForeignKey<Product>(e => e.ImageId);

        entity.Property(e => e.BLOB)
            .IsRequired();
        
        entity.Property(e => e.Enabled)
            .IsRequired();
    }
}

