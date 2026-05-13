using Shared.Core.Entities;

namespace Premises.Core.Extensions;

public static class PremiseQueryableExtensions
{
    public static IQueryable<Premise> WhereCompany(this IQueryable<Premise> queryable, int? companyId)
    {
        if (companyId is null) return queryable;
        return queryable.Where(p => p.CompanyId == companyId);
    }

    public static IQueryable<Premise> WhereEnabled(this IQueryable<Premise> queryable, bool? enabled)
    {
        if (enabled is null) return queryable;
        return queryable.Where(p => p.Enabled == enabled);
    }
}
