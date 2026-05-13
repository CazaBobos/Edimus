using Mediator;
using Shared.Core.Abstractions;

namespace Layouts.Core.Features.RestoreLayout;

public class RestoreLayoutRequest : IRequest<RestoreLayoutResponse>
{
    public int Id { get; set; }
    public IUserRecord User { get; set; } = null!;
}
