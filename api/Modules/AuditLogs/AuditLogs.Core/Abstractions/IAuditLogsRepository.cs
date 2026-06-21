using Shared.Core.Entities;

namespace AuditLogs.Core.Abstractions;

public interface IAuditLogsRepository
{
    IQueryable<AuditLog> AsQueryable();
}
