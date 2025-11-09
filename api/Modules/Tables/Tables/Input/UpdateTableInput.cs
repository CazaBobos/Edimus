using Shared.Core.Entities;

namespace Tables.Input;
public class UpdateTableInput
{
    public TableStatus? Status { get; set; }
    public int? PositionX { get; set; }
    public int? PositionY { get; set; }
    public List<(int, int)>? Surface { get; set; }
}
