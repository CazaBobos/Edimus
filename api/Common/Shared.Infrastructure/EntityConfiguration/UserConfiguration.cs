using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Core.Entities;

namespace Shared.Infrastructure.EntityConfiguration;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {

        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id)
            .HasColumnName("UserId")
            .ValueGeneratedOnAdd();

        entity.HasIndex(e => e.Username)
            .IsUnique();
        entity.Property(e => e.Username)
            .IsRequired()
            .HasMaxLength(32);

        entity.HasIndex(e => e.Email)
            .IsUnique();
        entity.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(64);

        entity.Property(e => e.Password)
            .IsRequired()
            .HasMaxLength(128);

        entity.Property(e => e.Role)
            .IsRequired();

        entity.Property(e => e.Enabled)
            .IsRequired();
    }
}

