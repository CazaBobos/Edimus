using Shared.Core.Entities;
using Tables.Core.Model;

namespace Tables.Input;
public class CreateTableInput
{
    public int LayoutId { get; set; }
    public TableStatus Status { get; set; } = TableStatus.Free;
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public List<TableCoordModel> Surface { get; set; } = new();
}
