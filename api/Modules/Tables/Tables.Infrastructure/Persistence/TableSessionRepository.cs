using Microsoft.EntityFrameworkCore;
using Shared.Core.Entities;
using Shared.Infrastructure.Persistence;
using Tables.Core.Abstractions;

namespace Tables.Infrastructure.Persistence;

public class TableSessionRepository : ITableSessionRepository
{
    private readonly DatabaseContext _context;

    public TableSessionRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task Add(TableSession session, CancellationToken cancellationToken = default)
    {
        await _context.TableSessions.AddAsync(session, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(TableSession session, CancellationToken cancellationToken = default)
    {
        _context.TableSessions.Update(session);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<TableSession?> GetActiveForTable(int tableId, CancellationToken cancellationToken = default)
    {
        return await _context.TableSessions
            .Where(s => s.TableId == tableId && s.ClosedAt == null)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
