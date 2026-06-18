using Mediator;
using Shared.Core.Abstractions;

namespace Tags.Core.Features.CreateTag;

public class CreateTagRequest : IRequest<CreateTagResponse>
{
    public int CompanyId { get; set; }
    public string Name { get; set; } = string.Empty;
    public IUserRecord User { get; set; } = null!;
}
