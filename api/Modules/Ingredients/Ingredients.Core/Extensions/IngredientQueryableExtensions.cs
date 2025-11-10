using Shared.Core.Entities;

namespace Ingredients.Core.Extensions;

public static class IngredientQueryableExtensions
{
    public static IQueryable<Ingredient> WhereName(this IQueryable<Ingredient> queryable, string? name)
    {
        if (name is null) return queryable;

        return queryable.Where(ingredient => ingredient.Name.Contains(name));
    }

    public static IQueryable<Ingredient> WhereUnit(this IQueryable<Ingredient> queryable, MeasurementUnit? unit)
    {
        if (unit is null) return queryable;

        return queryable.Where(ingredient => ingredient.Unit == unit);
    }

    public static IQueryable<Ingredient> WhereStockRange(this IQueryable<Ingredient> queryable, decimal? min, decimal? max)
    {
        return queryable.Where(product =>
            (min == null || product.Stock >= min)
            && (max == null || product.Stock <= max)
        );
    }

    public static IQueryable<Ingredient> WhereAlertRange(this IQueryable<Ingredient> queryable, decimal? min, decimal? max)
    {
        return queryable.Where(product =>
            (min == null || product.Alert >= min)
            && (max == null || product.Alert <= max)
        );
    }

    public static IQueryable<Ingredient> WhereEnabled(this IQueryable<Ingredient> queryable, bool? enabled)
    {
        if (enabled is null) return queryable;

        return queryable.Where(ingredient => ingredient.Enabled == enabled);
    }
}