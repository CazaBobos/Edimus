using Mediator;
using Shared.Core.Abstractions;

namespace Tags.Core.Features.UpdateTag;

public class UpdateTagRequest : IRequest<UpdateTagResponse>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public IUserRecord User { get; set; } = null!;
}
