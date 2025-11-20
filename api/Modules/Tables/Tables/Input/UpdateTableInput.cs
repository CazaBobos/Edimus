using Shared.Core.Entities;
using Tables.Core.Model;

namespace Tables.Input;
public class UpdateTableInput
{
    public TableStatus? Status { get; set; }
    public int? PositionX { get; set; }
    public int? PositionY { get; set; }
    public List<TableCoordModel>? Surface { get; set; }
}
