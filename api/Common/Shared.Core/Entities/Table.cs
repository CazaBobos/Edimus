using Dawn;
using Shared.Core.Domain;
using Shared.Core.Events;
using Shared.Core.Services;
namespace Shared.Core.Entities;

public class Table : AggregateRoot<int>
{
    public override int Id { get; protected set; }
    public int LayoutId { get; protected set; }
    public virtual Layout? Layout { get; protected set; }
    public TableStatus Status { get; protected set; }
    public string QrId { get; protected set; } = string.Empty;
    public int PositionX { get; protected set; }
    public int PositionY { get; protected set; }
    public virtual List<TableCoord> Surface { get; protected set; } = [];
    public virtual List<Order> Orders { get; protected set; } = [];

    protected Table() { }
    public Table(int layoutId, int positionX, int positionY, List<(int, int)>? surface = null, TableStatus status = TableStatus.Free)
    {
        LayoutId = Guard.Argument(() => layoutId).Positive();
        PositionX = Guard.Argument(() => positionX).NotNegative();
        PositionY = Guard.Argument(() => positionY).NotNegative();
        QrId = HashService.CreateHash(Id.ToString());

        if (surface is not null)
        {
            Guard.Argument(() => surface).Require(surface => surface.Any(s => s.Item1 == 0 && s.Item2 == 0));
            Surface = surface!.Select(s => new TableCoord(s.Item1, s.Item2, Id)).ToList();
        }

        Status = status;
        Enabled = true;
    }

    public void Arrive()
    {
        if (Status == TableStatus.Free) {
            Status = TableStatus.Arrived;
        }
    }

    public void Update(
        TableStatus? status = null,
        int? positionX = null,
        int? positionY = null,
        List<(int, int)>? surface = null,
        List<(int, int)>? orders = null
        )
    {
        var affectedMembers = new List<string>();

        if (status is not null && status != Status)
        {
            Guard.Operation(status != TableStatus.Arrived, "Cannot manually set a table to Arrived. Use the link endpoint instead.");
            Status = (TableStatus)status;

            if (status == TableStatus.Free) Orders.Clear();

            affectedMembers.Add(nameof(Status));
        }
        if (positionX is not null && positionX != PositionX)
        {
            PositionX = (int)Guard.Argument(() => positionX).NotNegative();
            affectedMembers.Add(nameof(PositionX));
        }
        if (positionY is not null && positionY != PositionY)
        {
            PositionY = (int)Guard.Argument(() => positionY).NotNegative();
            affectedMembers.Add(nameof(PositionY));
        }
        if (surface is not null)
        {
            Guard.Argument(() => surface).Require(surface => surface.Any(s => s.Item1 == 0 && s.Item2 == 0));
            Surface.Clear();
            Surface.AddRange(surface.Select(s => new TableCoord(s.Item1, s.Item2, Id)));
            affectedMembers.Add(nameof(Surface));
        }
        if (orders is not null && status != TableStatus.Free)
        {
            Guard.Argument(() => orders).Require(x => x.All(s => s.Item1 > 0 && s.Item2 > 0));

            var oldOrders = Orders.ToDictionary(o => o.ProductId, o => o.Amount);

            Orders.Clear();
            Orders.AddRange(orders.Select(r => new Order(productId: r.Item1, tableId: Id, amount: r.Item2)));

            var delta = orders
                .Select(o => (o.Item1, Amount: o.Item2 - oldOrders.GetValueOrDefault(o.Item1, 0)))
                .Where(d => d.Amount > 0)
                .Select(d => (ProductId: d.Item1, d.Amount))
                .ToList();

            if (delta.Count > 0)
                AddDomainEvent(new OrdersUpdatedEvent { Delta = delta });

            affectedMembers.Add(nameof(Orders));
        }
        //if (affectedMembers.Count != 0) AddHistory(user, AuditOperation.Updated, affectedMembers);
    }
}
