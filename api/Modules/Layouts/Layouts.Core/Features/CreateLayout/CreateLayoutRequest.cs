using Mediator;

namespace Layouts.Core.Features.CreateLayout;

public class CreateLayoutRequest : IRequest<CreateLayoutResponse>
{
    public int PremiseId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Height { get; set; }
    public int Width { get; set; }
}
