using MediatR;
using Shared.Core.Abstractions;

namespace Sectors.Core.Features.RemoveSector;

public class RemoveSectorRequest : IRequest<RemoveSectorResponse>
{
    public int Id { get; set; }
    public IUserRecord? User { get; set; }
}