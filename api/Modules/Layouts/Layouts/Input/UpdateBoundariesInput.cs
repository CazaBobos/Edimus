using Shared.Core.Entities;

namespace Layouts.Input;

public class UpdateBoundariesInput
{
    public List<BoundaryCoordInput> Boundaries { get; set; } = [];
}

public class BoundaryCoordInput
{
    public int X { get; set; }
    public int Y { get; set; }
    public LayoutCoordType Type { get; set; }
}
