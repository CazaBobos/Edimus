using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.EntityConfiguration;

namespace Shared.Infrastructure.Extensions;
public static class ModelBuilderExtensions
{
    public static void ConfigureTables(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AuditLogConfiguration());
        modelBuilder.ApplyConfiguration(new AuditLogChangeConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        modelBuilder.ApplyConfiguration(new ConsumptionConfiguration());
        modelBuilder.ApplyConfiguration(new PremiseConfiguration());
        modelBuilder.ApplyConfiguration(new ImageConfiguration());
        modelBuilder.ApplyConfiguration(new IngredientConfiguration());
        modelBuilder.ApplyConfiguration(new LayoutConfiguration());
        modelBuilder.ApplyConfiguration(new LayoutCoordConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new SectorConfiguration());
        modelBuilder.ApplyConfiguration(new SectorCoordConfiguration());
        modelBuilder.ApplyConfiguration(new TableConfiguration());
        modelBuilder.ApplyConfiguration(new TableCoordConfiguration());
        modelBuilder.ApplyConfiguration(new TableSessionConfiguration());
        modelBuilder.ApplyConfiguration(new SessionOrderConfiguration());
        modelBuilder.ApplyConfiguration(new TagConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}
