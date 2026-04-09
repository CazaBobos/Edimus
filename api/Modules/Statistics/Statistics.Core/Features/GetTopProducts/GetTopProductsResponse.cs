using Statistics.Core.Abstractions;

namespace Statistics.Core.Features.GetTopProducts;

public class GetTopProductsResponse
{
    public List<TopProduct> Data { get; set; } = [];
}
