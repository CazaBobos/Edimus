using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Core.Entities;

namespace Shared.Infrastructure.EntityConfiguration;
public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> entity)
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id)
            .HasColumnName("CompanyId")
            .ValueGeneratedOnAdd();

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(32);
        entity.Property(e => e.Slogan)
            .IsRequired()
            .HasMaxLength(64);
        entity.Property(e => e.Acronym)
            .IsRequired()
            .HasMaxLength(8);

        entity.HasMany(e => e.Establishments)
            .WithOne(e => e.Company)
            .HasForeignKey(e => e.CompanyId);

        entity.HasMany(e => e.Categories)
            .WithOne(e => e.Company)
            .HasForeignKey(e => e.CompanyId);

        entity.Property(e => e.Enabled)
            .IsRequired();
    }
}

