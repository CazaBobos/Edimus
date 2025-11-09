using Products.Core.Model;

namespace Products.Core.Features.GetOneProduct;
public class GetOneProductResponse
{
    public ProductModel Product { get; set; } = new();
}
