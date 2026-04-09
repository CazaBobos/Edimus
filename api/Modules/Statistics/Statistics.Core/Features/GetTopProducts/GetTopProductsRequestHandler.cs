using Mediator;
using Statistics.Core.Abstractions;

namespace Statistics.Core.Features.GetTopProducts;

public class GetTopProductsRequestHandler : IRequestHandler<GetTopProductsRequest, GetTopProductsResponse>
{
    private readonly IStatisticsRepository _repository;

    public GetTopProductsRequestHandler(IStatisticsRepository repository) => _repository = repository;

    public async ValueTask<GetTopProductsResponse> Handle(GetTopProductsRequest request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetTopProducts(request.From.Date, request.To.Date.AddDays(1), request.Limit, cancellationToken);
        return new GetTopProductsResponse { Data = data };
    }
}
