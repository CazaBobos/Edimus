using MediatR;
using Sectors.Core.Model;
using Shared.Core.Abstractions;

namespace Sectors.Core.Features.CreateSector;

public class CreateSectorRequest : IRequest<CreateSectorResponse>
{
    public int LayoutId { get; set; }
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public List<SectorCoordModel> Surface { get; set; } = new();
    public IUserRecord? User { get; set; }
}