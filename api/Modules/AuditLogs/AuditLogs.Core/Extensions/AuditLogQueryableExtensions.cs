using Shared.Core.Domain;
using Shared.Core.Entities;

namespace AuditLogs.Core.Extensions;

public static class AuditLogQueryableExtensions
{
    public static IQueryable<AuditLog> WhereDateFrom(this IQueryable<AuditLog> queryable, DateTime? dateFrom)
    {
        if (dateFrom is null) return queryable;
        var utc = DateTime.SpecifyKind(dateFrom.Value, DateTimeKind.Utc);
        return queryable.Where(l => l.DateTime >= utc);
    }

    public static IQueryable<AuditLog> WhereDateTo(this IQueryable<AuditLog> queryable, DateTime? dateTo)
    {
        if (dateTo is null) return queryable;
        var utc = DateTime.SpecifyKind(dateTo.Value.Date.AddDays(1).AddTicks(-1), DateTimeKind.Utc);
        return queryable.Where(l => l.DateTime <= utc);
    }

    public static IQueryable<AuditLog> WhereEntityType(this IQueryable<AuditLog> queryable, string? entityType)
    {
        if (string.IsNullOrWhiteSpace(entityType)) return queryable;
        return queryable.Where(l => l.EntityType == entityType);
    }

    public static IQueryable<AuditLog> WhereUsername(this IQueryable<AuditLog> queryable, string? username)
    {
        if (string.IsNullOrWhiteSpace(username)) return queryable;
        return queryable.Where(l => l.Username.Contains(username));
    }

    public static IQueryable<AuditLog> WhereOperation(this IQueryable<AuditLog> queryable, AuditOperation? operation)
    {
        if (operation is null) return queryable;
        return queryable.Where(l => l.Operation == operation.Value);
    }
}
