using Mediator;
using Shared.Core.Abstractions;

namespace Statistics.Core.Features.GetOccupancy;

public class GetOccupancyRequest : IRequest<GetOccupancyResponse>
{
    public DateTime Date { get; set; }
    public IUserRecord? User { get; set; }
}
