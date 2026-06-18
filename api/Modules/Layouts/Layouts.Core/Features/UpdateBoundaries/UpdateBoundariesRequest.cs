using Mediator;
using Shared.Core.Entities;

namespace Layouts.Core.Features.UpdateBoundaries;

public class UpdateBoundariesRequest : IRequest<UpdateBoundariesResponse>
{
    public int LayoutId { get; set; }
    public List<(int X, int Y, LayoutCoordType Type)> Boundaries { get; set; } = [];
}
