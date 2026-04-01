using Microsoft.AspNetCore.SignalR;

namespace Tables.Hubs;

public class TablesHub : Hub
{
    /// <summary>
    /// Subscribes the caller to updates for a specific layout.
    /// Clients should call this after connecting.
    /// </summary>
    public async Task JoinLayout(int layoutId)
        => await Groups.AddToGroupAsync(Context.ConnectionId, GroupName(layoutId));

    /// <summary>
    /// Unsubscribes the caller from updates for a specific layout.
    /// </summary>
    public async Task LeaveLayout(int layoutId)
        => await Groups.RemoveFromGroupAsync(Context.ConnectionId, GroupName(layoutId));

    public static string GroupName(int layoutId) => $"layout-{layoutId}";
}
