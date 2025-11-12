using Shared.Core.Entities;

namespace Sectors.Input;
public class UpdateSectorInput
{
    public TableStatus? Status { get; set; }
    public int? PositionX { get; set; }
    public int? PositionY { get; set; }
    public string? Name { get; set; }
    public string? Color { get; set; }
    public List<(int, int)>? Surface { get; set; }
}
