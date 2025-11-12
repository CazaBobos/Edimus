using Shared.Core.Entities;

namespace Tables.Core.Model;
public class TableModel
{
    public long Id { get; set; }
    public int LayoutId { get; set; }
    public TableStatus Status { get; set; }
    public string QR { get; set; } = string.Empty;
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public List<TableCoordModel> Surface { get; set; } = new();
}