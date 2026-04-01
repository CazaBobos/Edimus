using Microsoft.AspNetCore.SignalR;
using Shared.Core.Entities;
using Tables.Core.Abstractions;

namespace Tables.Hubs;

public class TableHubNotifier : ITableNotifier
{
    private readonly IHubContext<TablesHub> _hub;

    public TableHubNotifier(IHubContext<TablesHub> hub) => _hub = hub;

    public async Task NotifyStatusChanged(int tableId, int layoutId, TableStatus status, CancellationToken ct = default)
        => await _hub.Clients
            .Group(TablesHub.GroupName(layoutId))
            .SendAsync("TableStatusChanged", new { tableId, status = status.ToString() }, ct);
}
