using Mediator;
using Sectors.Core.Model;

namespace Sectors.Core.Features.UpdateSector;

public class UpdateSectorRequest : IRequest<UpdateSectorResponse>
{
    public int Id { get; set; }
    public int? PositionX { get; set; }
    public int? PositionY { get; set; }
    public string? Name { get; set; }
    public string? Color { get; set; }
    public List<SectorCoordModel>? Surface { get; set; }
}