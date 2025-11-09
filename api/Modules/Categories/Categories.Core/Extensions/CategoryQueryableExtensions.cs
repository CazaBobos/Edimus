using Shared.Core.Entities;

namespace Categories.Core.Extensions;

public static class CategoryQueryableExtensions
{
    public static IQueryable<Category> WhereCompany(this IQueryable<Category> queryable, long? companyId)
    {
        if (companyId is null) return queryable;

        return queryable.Where(category => category.CompanyId == companyId);
    }

    public static IQueryable<Category> WhereName(this IQueryable<Category> queryable, string? name)
    {
        if (name is null) return queryable;

        return queryable.Where(category => category.Name.Contains(name));
    }

    public static IQueryable<Category> WhereEnabled(this IQueryable<Category> queryable, bool? enabled)
    {
        if (enabled is null) return queryable;

        return queryable.Where(category => category.Enabled == enabled);
    }
}