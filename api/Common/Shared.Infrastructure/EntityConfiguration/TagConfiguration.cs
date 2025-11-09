using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Core.Entities;

namespace Shared.Infrastructure.EntityConfiguration;
public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> entity)
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id)
            .HasColumnName("TagId")
            .ValueGeneratedOnAdd();

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(32);

        entity.HasMany(e => e.Products)
            .WithMany(e => e.Tags);

        entity.Property(e => e.Enabled)
            .IsRequired();
    }
}

