using AuditLogs.Core.Abstractions;
using Shared.Core.Entities;
using Shared.Infrastructure.Persistence;

namespace AuditLogs.Infrastructure.Persistence;

public class AuditLogsRepository : IAuditLogsRepository
{
    private readonly DatabaseContext _context;

    public AuditLogsRepository(DatabaseContext context)
    {
        _context = context;
    }

    public IQueryable<AuditLog> AsQueryable() => _context.AuditLogs.AsQueryable();
}
