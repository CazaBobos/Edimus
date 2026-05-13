using Mediator;
using Shared.Core.Abstractions;

namespace Layouts.Core.Features.RemoveLayout;

public class RemoveLayoutRequest : IRequest<RemoveLayoutResponse>
{
    public int Id { get; set; }
    public IUserRecord User { get; set; } = null!;
}
