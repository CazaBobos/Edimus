using Mediator;

namespace Statistics.Core.Features.GetOccupancy;

public class GetOccupancyRequest : IRequest<GetOccupancyResponse>
{
    public DateTime Date { get; set; }
}
