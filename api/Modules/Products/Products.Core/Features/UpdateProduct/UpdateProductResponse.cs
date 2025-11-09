using Products.Core.Model;

namespace Products.Core.Features.CreateProduct;
public class UpdateProductResponse
{
    public ProductModel Product { get; set; } = new();
}