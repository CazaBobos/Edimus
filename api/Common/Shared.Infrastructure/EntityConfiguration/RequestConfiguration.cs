using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Core.Entities;

namespace Shared.Infrastructure.EntityConfiguration;
public class RequestConfiguration : IEntityTypeConfiguration<Request>
{

    public void Configure(EntityTypeBuilder<Request> entity)
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id)
            .HasColumnName("RequestId")
            .ValueGeneratedOnAdd();

        entity.Property(e => e.ProductId)
            .IsRequired();

        entity.Property(e => e.TableId)
            .IsRequired();

        entity.Property(e => e.Quantity)
            .IsRequired();

        entity.Property(e => e.Enabled)
            .IsRequired();
    }
}

