using Mediator;
using Shared.Core.Abstractions;

namespace Tags.Core.Features.RemoveTag;

public class RemoveTagRequest : IRequest<RemoveTagResponse>
{
    public int Id { get; set; }
    public IUserRecord User { get; set; } = null!;
}
