using Shared.Core.Entities;

namespace Tags.Core.Extensions;

public static class TagQueryableExtensions
{
    public static IQueryable<Tag> WhereCompany(this IQueryable<Tag> queryable, int? companyId)
    {
        if (companyId is null) return queryable;

        return queryable.Where(tag => tag.CompanyId == companyId);
    }

    public static IQueryable<Tag> WhereName(this IQueryable<Tag> queryable, string? name)
    {
        if (name is null) return queryable;

        return queryable.Where(tag => tag.Name.Contains(name));
    }

    public static IQueryable<Tag> WhereEnabled(this IQueryable<Tag> queryable, bool? enabled)
    {
        if (enabled is null) return queryable;

        return queryable.Where(tag => tag.Enabled == enabled);
    }
}
