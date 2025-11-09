using Shared.Core.Entities;

namespace Tables.Input;
public class CreateTableInput
{
    public int LayoutId { get; set; }
    public TableStatus Status { get; set; } = TableStatus.Free;
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public List<(int, int)> Surface { get; set; } = new(){(0,0)};
}
