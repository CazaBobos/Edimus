using Mediator;

namespace Shared.Core.Notifications;

public class OrdersConfirmedNotification : INotification
{
    public List<(int ProductId, int Amount)> Delta { get; set; } = [];
}
