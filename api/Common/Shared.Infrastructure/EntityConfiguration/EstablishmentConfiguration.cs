using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Core.Entities;

namespace Shared.Infrastructure.EntityConfiguration;
public class EstablishmentConfiguration : IEntityTypeConfiguration<Establishment>
{
    public void Configure(EntityTypeBuilder<Establishment> entity)
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id)
            .HasColumnName("EstablishmentId")
            .ValueGeneratedOnAdd();

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(32);

        entity.Property(e => e.CompanyId)
            .IsRequired();
        entity.HasOne(e => e.Company)
            .WithMany(e => e.Establishments)
            .HasForeignKey(e => e.CompanyId);

        entity.HasMany(e => e.Layouts)
            .WithOne(e => e.Establishment)
            .HasForeignKey(e => e.EstablishmentId);
    }
}

