using Shared.Core.Entities;

namespace Tables.Core.Extensions;

public static class TableQueryableExtensions
{    
    public static IQueryable<Table> WhereEnabled(this IQueryable<Table> queryable, bool? enabled)
    {
        if(enabled is null) return queryable;

        return queryable.Where(table => table.Enabled == enabled);
    }
}