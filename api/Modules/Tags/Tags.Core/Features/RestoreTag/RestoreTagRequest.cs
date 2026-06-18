using Mediator;

namespace Tags.Core.Features.RestoreTag;

public class RestoreTagRequest : IRequest<RestoreTagResponse>
{
    public int Id { get; set; }
}
