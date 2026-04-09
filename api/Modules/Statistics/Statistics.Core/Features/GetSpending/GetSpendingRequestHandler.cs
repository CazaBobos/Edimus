using Mediator;
using Statistics.Core.Abstractions;

namespace Statistics.Core.Features.GetSpending;

public class GetSpendingRequestHandler : IRequestHandler<GetSpendingRequest, GetSpendingResponse>
{
    private readonly IStatisticsRepository _repository;

    public GetSpendingRequestHandler(IStatisticsRepository repository) => _repository = repository;

    public async ValueTask<GetSpendingResponse> Handle(GetSpendingRequest request, CancellationToken cancellationToken)
    {
        var avg = await _repository.GetAverageSpending(request.From.Date, request.To.Date.AddDays(1), cancellationToken);
        return new GetSpendingResponse { AveragePerSession = avg };
    }
}
