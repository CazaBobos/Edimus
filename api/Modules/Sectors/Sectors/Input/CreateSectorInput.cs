using Shared.Core.Entities;

namespace Sectors.Input;
public class CreateSectorInput
{
    public int LayoutId { get; set; }
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public List<(int, int)> Surface { get; set; } = new(){(0,0)};
}
