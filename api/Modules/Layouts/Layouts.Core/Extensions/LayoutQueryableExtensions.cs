using Shared.Core.Entities;

namespace Layouts.Core.Extensions;

public static class LayoutQueryableExtensions
{
    public static IQueryable<Layout> WherePremise(this IQueryable<Layout> queryable, int? premiseId)
    {
        if (premiseId is null) return queryable;
        return queryable.Where(l => l.PremiseId == premiseId);
    }

    public static IQueryable<Layout> WhereEnabled(this IQueryable<Layout> queryable, bool? enabled)
    {
        if (enabled is null) return queryable;
        return queryable.Where(l => l.Enabled == enabled);
    }
}
