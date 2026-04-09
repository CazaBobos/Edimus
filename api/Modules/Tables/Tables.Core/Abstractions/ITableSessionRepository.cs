using Shared.Core.Entities;

namespace Tables.Core.Abstractions;

public interface ITableSessionRepository
{
    Task Add(TableSession session, CancellationToken cancellationToken = default);
    Task Update(TableSession session, CancellationToken cancellationToken = default);
    Task<TableSession?> GetActiveForTable(int tableId, CancellationToken cancellationToken = default);
}
