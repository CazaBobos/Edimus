using Mediator;
using Shared.Core.Abstractions;

namespace Tags.Core.Features.RestoreTag;

public class RestoreTagRequest : IRequest<RestoreTagResponse>
{
    public int Id { get; set; }
    public IUserRecord User { get; set; } = null!;
}
