using Products.Core.Model;

namespace Products.Core.Features.GetProducts;

public class GetProductsResponse
{
    public int Count { get; set; }
    public int? Limit { get; set; }
    public int? Page { get; set; }
    public List<ProductModel> Products { get; set; } = new();
}