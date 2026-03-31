using Mediator;

namespace Shared.Core.Events;

public class OrdersUpdatedEvent : INotification
{
    public List<(int ProductId, int Amount)> Delta { get; set; } = [];
}
