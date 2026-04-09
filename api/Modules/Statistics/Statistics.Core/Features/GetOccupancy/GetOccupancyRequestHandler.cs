using Mediator;
using Statistics.Core.Abstractions;

namespace Statistics.Core.Features.GetOccupancy;

public class GetOccupancyRequestHandler : IRequestHandler<GetOccupancyRequest, GetOccupancyResponse>
{
    private readonly IStatisticsRepository _repository;

    public GetOccupancyRequestHandler(IStatisticsRepository repository) => _repository = repository;

    public async ValueTask<GetOccupancyResponse> Handle(GetOccupancyRequest request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetHourlyOccupancy(request.Date.Date, cancellationToken);
        return new GetOccupancyResponse { Data = data };
    }
}
