using Dawn;
using Shared.Core.Domain;
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
    public virtual List<TableCoord>? Surface { get; protected set; }
    public virtual List<Request>? Requests { get; protected set; }

    protected Table() { }
    public Table(int layoutId, int positionX, int positionY, List<(int, int)>? surface = null, TableStatus status = TableStatus.Free)
    {
        LayoutId = Guard.Argument(() => layoutId).Positive();
        PositionX = Guard.Argument(() => positionX).NotNegative();
        PositionY = Guard.Argument(() => positionY).NotNegative();
        QrId = HashService.CreateHash(Id.ToString());

        if (surface is not null)
        {
            Guard.Argument(() => surface).MinCount(1);
            Surface = surface!.Select(s => new TableCoord(s.Item1, s.Item2, Id)).ToList();
        }
        
        Status = status;
        Enabled = true;
    }

    public void Update(TableStatus? status, int? positionX, int? positionY, List<(int, int)>? surface)
    {
        var affectedMembers = new List<string>();

        if (status is not null && status != Status)
        {
            Status = (TableStatus)status;

            if (status == TableStatus.Free) Requests = new();

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
            Surface = surface.Select(s => new TableCoord(s.Item1, s.Item2, Id)).ToList();
            affectedMembers.Add(nameof(Surface));
        }
        //if (affectedMembers.Count != 0) AddHistory(user, AuditOperation.Updated, affectedMembers);
    }
}
