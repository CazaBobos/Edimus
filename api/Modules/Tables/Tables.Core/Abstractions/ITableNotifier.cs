using Shared.Core.Entities;

namespace Tables.Core.Abstractions;

public interface ITableNotifier
{
    Task NotifyStatusChanged(int tableId, int layoutId, TableStatus status, CancellationToken ct = default);
}
