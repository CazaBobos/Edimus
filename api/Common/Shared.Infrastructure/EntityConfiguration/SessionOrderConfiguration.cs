using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Core.Entities;

namespace Shared.Infrastructure.EntityConfiguration;

public class SessionOrderConfiguration : IEntityTypeConfiguration<SessionOrder>
{
    public void Configure(EntityTypeBuilder<SessionOrder> entity)
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).ValueGeneratedOnAdd();

        entity.Property(e => e.SessionId).IsRequired();
        entity.Property(e => e.ProductId).IsRequired();
        entity.Property(e => e.ProductName).IsRequired().HasMaxLength(128);
        entity.Property(e => e.UnitPrice).IsRequired().HasPrecision(18, 2);
        entity.Property(e => e.Amount).IsRequired();
    }
}
