using Shared.Core.Entities;

namespace Tables.Core.Extensions;

public static class TableQueryableExtensions
{
    public static IQueryable<Table> WhereLayout(this IQueryable<Table> queryable, int? layout)
    {
        if (layout is null) return queryable;

        return queryable.Where(table => table.LayoutId == layout);
    }

    public static IQueryable<Table> WhereStatus(this IQueryable<Table> queryable, TableStatus? status)
    {
        if(status is null) return queryable;

        return queryable.Where(table => table.Status == status);
    }

    public static IQueryable<Table> WhereEnabled(this IQueryable<Table> queryable, bool? enabled)
    {
        if(enabled is null) return queryable;

        return queryable.Where(table => table.Enabled == enabled);
    }
}