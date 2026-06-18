using Mediator;

namespace Products.Core.Features.RestoreProduct;
public class RestoreProductRequest : IRequest<RestoreProductResponse>
{
    public int Id { get; set; }
}
