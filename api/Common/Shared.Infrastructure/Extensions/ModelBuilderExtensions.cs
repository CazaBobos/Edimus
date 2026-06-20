using Microsoft.EntityFrameworkCore;
using Shared.Core.Abstractions;
using Shared.Core.Entities;
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

    public static void ApplyQueryFilters(this ModelBuilder modelBuilder, ICurrentCompanyService currentCompanyService)
    {
        modelBuilder.Entity<Product>().HasQueryFilter(p =>
            !currentCompanyService.AllowedCompanyIds.Any() || currentCompanyService.AllowedCompanyIds.Contains(p.CompanyId));
        modelBuilder.Entity<Tag>().HasQueryFilter(t =>
            !currentCompanyService.AllowedCompanyIds.Any() || currentCompanyService.AllowedCompanyIds.Contains(t.CompanyId));
        modelBuilder.Entity<Ingredient>().HasQueryFilter(i =>
            !currentCompanyService.AllowedCompanyIds.Any() || currentCompanyService.AllowedCompanyIds.Contains(i.CompanyId));
        modelBuilder.Entity<Category>().HasQueryFilter(c =>
            !currentCompanyService.AllowedCompanyIds.Any() || currentCompanyService.AllowedCompanyIds.Contains(c.CompanyId));
        modelBuilder.Entity<Layout>().HasQueryFilter(l =>
            !currentCompanyService.AllowedCompanyIds.Any() || currentCompanyService.AllowedCompanyIds.Contains(l.Premise!.CompanyId));
        modelBuilder.Entity<Sector>().HasQueryFilter(s =>
            !currentCompanyService.AllowedCompanyIds.Any() || currentCompanyService.AllowedCompanyIds.Contains(s.Layout!.Premise!.CompanyId));
        modelBuilder.Entity<Table>().HasQueryFilter(t =>
            !currentCompanyService.AllowedCompanyIds.Any() || currentCompanyService.AllowedCompanyIds.Contains(t.Layout!.Premise!.CompanyId));
    }
}
