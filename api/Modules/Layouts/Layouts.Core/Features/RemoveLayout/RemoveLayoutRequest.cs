using Mediator;

namespace Layouts.Core.Features.RemoveLayout;

public class RemoveLayoutRequest : IRequest<RemoveLayoutResponse>
{
    public int Id { get; set; }
}
