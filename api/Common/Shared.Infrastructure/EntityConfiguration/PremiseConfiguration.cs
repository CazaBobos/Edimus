using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Core.Entities;

namespace Shared.Infrastructure.EntityConfiguration;
public class PremiseConfiguration : IEntityTypeConfiguration<Premise>
{
    public void Configure(EntityTypeBuilder<Premise> entity)
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id)
            .HasColumnName("PremiseId")
            .ValueGeneratedOnAdd();

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(32);

        entity.Property(e => e.CompanyId)
            .IsRequired();
        entity.HasOne(e => e.Company)
            .WithMany(e => e.Premises)
            .HasForeignKey(e => e.CompanyId);

        entity.HasMany(e => e.Layouts)
            .WithOne(e => e.Premise)
            .HasForeignKey(e => e.PremiseId);
    }
}

