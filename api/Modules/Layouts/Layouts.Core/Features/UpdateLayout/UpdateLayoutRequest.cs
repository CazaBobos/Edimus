using Mediator;
using Shared.Core.Abstractions;

namespace Layouts.Core.Features.UpdateLayout;

public class UpdateLayoutRequest : IRequest<UpdateLayoutResponse>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int? Height { get; set; }
    public int? Width { get; set; }
    public IUserRecord User { get; set; } = null!;
}
