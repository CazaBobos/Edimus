using Mediator;
using Statistics.Core.Abstractions;

namespace Statistics.Core.Features.GetAttentionTimes;

public class GetAttentionTimesRequestHandler : IRequestHandler<GetAttentionTimesRequest, GetAttentionTimesResponse>
{
    private readonly IStatisticsRepository _repository;

    public GetAttentionTimesRequestHandler(IStatisticsRepository repository) => _repository = repository;

    public async ValueTask<GetAttentionTimesResponse> Handle(GetAttentionTimesRequest request, CancellationToken cancellationToken)
    {
        var times = await _repository.GetAttentionTimes(request.From.Date, request.To.Date.AddDays(1), cancellationToken);
        return new GetAttentionTimesResponse
        {
            AverageArrivalSeconds = times.AverageArrivalSeconds,
            AverageCallingSeconds = times.AverageCallingSeconds,
        };
    }
}
