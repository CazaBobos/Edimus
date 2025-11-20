using Sectors.Core.Model;

namespace Sectors.Input;
public class CreateSectorInput
{
    public int LayoutId { get; set; }
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public List<SectorCoordModel> Surface { get; set; } = new();
}
