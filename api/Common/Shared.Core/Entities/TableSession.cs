namespace Shared.Core.Entities;

public class TableSession
{
    public long Id { get; private set; }
    public int TableId { get; private set; }
    public virtual Table? Table { get; private set; }
    public DateTime OpenedAt { get; private set; }
    public DateTime? ClosedAt { get; private set; }
    public DateTime? ArrivedAt { get; private set; }
    public int? ArrivalAttentionSeconds { get; private set; }
    public int TotalCallingSeconds { get; private set; }
    public int CallingCount { get; private set; }
    public virtual List<SessionOrder> Orders { get; private set; } = [];

    protected TableSession() { }

    public TableSession(int tableId, DateTime openedAt, DateTime? arrivedAt, int? arrivalAttentionSeconds)
    {
        TableId = tableId;
        OpenedAt = openedAt;
        ArrivedAt = arrivedAt;
        ArrivalAttentionSeconds = arrivalAttentionSeconds;
    }

    public void AddCallingTime(int seconds)
    {
        TotalCallingSeconds += seconds;
        CallingCount++;
    }

    public void Close(DateTime closedAt, List<SessionOrder> orders)
    {
        ClosedAt = closedAt;
        Orders.AddRange(orders);
    }
}
