using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Core.Entities;

namespace Shared.Infrastructure.EntityConfiguration;
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> entity)
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id)
            .HasColumnName("CategoryId")
            .ValueGeneratedOnAdd();

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(48);

        entity.Property(e => e.CompanyId)
            .IsRequired();
        entity.HasOne(e => e.Company)
            .WithMany(e => e.Categories)
            .HasForeignKey(e => e.CompanyId);

        entity.HasMany(e => e.Products)
            .WithOne(c => c.Category)
            .HasForeignKey(c => c.CategoryId);

        entity.Property(e => e.Enabled)
            .IsRequired();
    }
}

