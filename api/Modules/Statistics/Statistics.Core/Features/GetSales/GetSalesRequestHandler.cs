using Mediator;
using Statistics.Core.Abstractions;

namespace Statistics.Core.Features.GetSales;

public class GetSalesRequestHandler : IRequestHandler<GetSalesRequest, GetSalesResponse>
{
    private readonly IStatisticsRepository _repository;

    public GetSalesRequestHandler(IStatisticsRepository repository) => _repository = repository;

    public async ValueTask<GetSalesResponse> Handle(GetSalesRequest request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetSales(request.From.Date, request.To.Date.AddDays(1), request.GroupBy, cancellationToken);
        return new GetSalesResponse { Data = data };
    }
}
