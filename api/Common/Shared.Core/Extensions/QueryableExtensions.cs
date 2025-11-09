using Dawn;

namespace Shared.Core.Extensions;
public static class QueryableExtensions
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, int? pageLimit, int? pageNumber)
    {
        Guard.Argument(() => pageLimit).Positive();
        Guard.Argument(() => pageNumber).Positive();

        if (pageLimit == null || pageNumber == null) return queryable;

        var amountToSkip = (int)(pageLimit * (pageNumber - 1));
        var amountToTake = (int)pageLimit;
        return queryable
            .Skip(amountToSkip)
            .Take(amountToTake);
    }
}
