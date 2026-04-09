using Mediator;
using Statistics.Core.Abstractions;

namespace Statistics.Core.Features.GetRotation;

public class GetRotationRequestHandler : IRequestHandler<GetRotationRequest, GetRotationResponse>
{
    private readonly IStatisticsRepository _repository;

    public GetRotationRequestHandler(IStatisticsRepository repository) => _repository = repository;

    public async ValueTask<GetRotationResponse> Handle(GetRotationRequest request, CancellationToken cancellationToken)
    {
        var avg = await _repository.GetAverageRotationMinutes(request.From.Date, request.To.Date.AddDays(1), cancellationToken);
        return new GetRotationResponse { AverageMinutes = avg };
    }
}
