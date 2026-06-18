using Mediator;

namespace Products.Core.Features.GetOneProduct;
public class GetOneProductRequest : IRequest<GetOneProductResponse>
{
    public int Id { get; set; }
}
