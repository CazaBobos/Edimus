using Shared.Core.Entities;

namespace Sectors.Core.Extensions;

public static class SectorQueryableExtensions
{
    public static IQueryable<Sector> WhereLayout(this IQueryable<Sector> queryable, int? layout)
    {
        if (layout is null) return queryable;

        return queryable.Where(sector => sector.LayoutId == layout);
    }

    public static IQueryable<Sector> WhereName(this IQueryable<Sector> queryable, string? name)
    {
        if(name is null) return queryable;

        return queryable.Where(sector => sector.Name == name);
    }

    public static IQueryable<Sector> WhereEnabled(this IQueryable<Sector> queryable, bool? enabled)
    {
        if(enabled is null) return queryable;

        return queryable.Where(sector => sector.Enabled == enabled);
    }
}