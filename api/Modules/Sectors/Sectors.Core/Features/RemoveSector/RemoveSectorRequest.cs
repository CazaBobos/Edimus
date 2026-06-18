using Mediator;

namespace Sectors.Core.Features.RemoveSector;

public class RemoveSectorRequest : IRequest<RemoveSectorResponse>
{
    public int Id { get; set; }
}