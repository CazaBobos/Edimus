using Shared.Core.Entities;

namespace Products.Core.Extensions;

public static class ProductQueryableExtensions
{
    public static IQueryable<Product> WhereName(this IQueryable<Product> queryable, string? name)
    {
        if (name is null) return queryable;

        return queryable.Where(product => product.Name.Contains(name));
    }

    public static IQueryable<Product> WhereDescription(this IQueryable<Product> queryable, string? description)
    {
        if (description is null) return queryable;

        return queryable.Where(product => product.Description.Contains(description));
    }

    public static IQueryable<Product> WhereCategories(this IQueryable<Product> queryable, List<int>? categories)
    {
        if (categories is null || categories.Count == 0) return queryable;

        return queryable.Where(product => product.CategoryId == null || categories.Contains((int)product.CategoryId));
    }

    public static IQueryable<Product> WherePriceRange(this IQueryable<Product> queryable, decimal? minPrice, decimal? maxPrice)
    {
        return queryable.Where(product =>
            (minPrice == null || product.Price >= minPrice)
            && (maxPrice == null || product.Price <= maxPrice)
        );
    }

    public static IQueryable<Product> WhereTags(this IQueryable<Product> queryable, List<int>? tags)
    {
        if (tags is null || tags.Count == 0) return queryable;

        return queryable.Where(product => 
            product.Tags == null ||
            product.Tags.Count == 0 ||
            product.Tags.Any(tag => tags.Contains(tag.Id))
        );
    }

    public static IQueryable<Product> WhereEnabled(this IQueryable<Product> queryable, bool? enabled)
    {
        if (enabled is null) return queryable;

        return queryable.Where(product => product.Enabled == enabled);
    }
}