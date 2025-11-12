using Dawn;

namespace Shared.Core.Entities;

public class SectorCoord
{
    public int X { get; protected set; }
    public int Y { get; protected set; }
    public int SectorId { get; protected set; }
    public virtual Sector? Sector{ get; protected set; }

    protected SectorCoord() { }
    public SectorCoord(int x, int y, int sectorId)
    {
        X = Guard.Argument(() => x).NotNegative();
        Y = Guard.Argument(() => y).NotNegative();
        SectorId = Guard.Argument(() => sectorId).Positive();
    }
}
