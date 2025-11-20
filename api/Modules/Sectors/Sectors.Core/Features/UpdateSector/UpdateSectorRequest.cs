using MediatR;
using Sectors.Core.Model;
using Shared.Core.Abstractions;

namespace Sectors.Core.Features.UpdateSector;

public class UpdateSectorRequest : IRequest<UpdateSectorResponse>
{
    public int Id { get; set; }
    public int? PositionX { get; set; }
    public int? PositionY { get; set; }
    public string? Name { get; set; }
    public string? Color { get; set; }
    public List<SectorCoordModel>? Surface { get; set; }
    public IUserRecord? User { get; set; }
}