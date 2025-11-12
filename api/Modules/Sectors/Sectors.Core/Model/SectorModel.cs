using Shared.Core.Entities;

namespace Sectors.Core.Model;
public class SectorModel
{
    public int Id { get; set; }
    public int LayoutId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public List<(int X, int Y)> Surface { get; set; } = new();
}