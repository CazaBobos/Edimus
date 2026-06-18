using Mediator;

namespace Products.Core.Features.RemoveProduct;
public class RemoveProductRequest : IRequest<RemoveProductResponse>
{
    public int Id { get; set; }
}
