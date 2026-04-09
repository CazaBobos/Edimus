using Mediator;
using Shared.Core.Abstractions;

namespace Statistics.Core.Features.GetTopProducts;

public class GetTopProductsRequest : IRequest<GetTopProductsResponse>
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public int Limit { get; set; } = 10;
    public IUserRecord? User { get; set; }
}
