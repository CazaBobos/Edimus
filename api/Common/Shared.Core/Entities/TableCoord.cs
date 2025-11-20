using Dawn;

namespace Shared.Core.Entities;

public class TableCoord
{
    public int X { get; protected set; }
    public int Y { get; protected set; }
    public int TableId { get; protected set; }
    public virtual Table? Table { get; protected set; }

    protected TableCoord() { }
    public TableCoord(int x, int y, int tableId)
    {
        X = Guard.Argument(() => x);
        Y = Guard.Argument(() => y);
        TableId = Guard.Argument(() => tableId).Positive();
    }
}
