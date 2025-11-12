using Dawn;

namespace Shared.Core.Entities;

public class LayoutCoord
{
    public int X { get; protected set; }
    public int Y { get; protected set; }
    public LayoutCoordType Type { get; protected set; }
    public int LayoutId { get; protected set; }
    public virtual Layout? Layout { get; protected set; }

    protected LayoutCoord() { }
    public LayoutCoord(int x, int y, int layoutId, LayoutCoordType type)
    {
        X = Guard.Argument(() => x).NotNegative();
        Y = Guard.Argument(() => y).NotNegative();
        LayoutId = Guard.Argument(() => layoutId).Positive();
        Type = type;
    }
}
