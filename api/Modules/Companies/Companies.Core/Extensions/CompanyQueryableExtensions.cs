using Shared.Core.Entities;

namespace Companies.Core.Extensions;

public static class CompanyQueryableExtensions
{
    public static IQueryable<Company> WhereName(this IQueryable<Company> queryable, string? name)
    {
        if(name is null) return queryable;

        return queryable.Where(company => company.Name.Contains(name));
    }

    public static IQueryable<Company> WhereSlogan(this IQueryable<Company> queryable, string? slogan)
    {
        if(slogan is null) return queryable;

        return queryable.Where(company => company.Slogan.Contains(slogan));
    }
    
    public static IQueryable<Company> WhereAcronym(this IQueryable<Company> queryable, string? acronym)
    {
        if(acronym is null) return queryable;

        return queryable.Where(company => company.Acronym.Contains(acronym));
    }
    
    public static IQueryable<Company> WhereEnabled(this IQueryable<Company> queryable, bool? enabled)
    {
        if(enabled is null) return queryable;

        return queryable.Where(company => company.Enabled == enabled);
    }
}