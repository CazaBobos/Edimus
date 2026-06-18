using Mediator;

namespace Tags.Core.Features.RemoveTag;

public class RemoveTagRequest : IRequest<RemoveTagResponse>
{
    public int Id { get; set; }
}
