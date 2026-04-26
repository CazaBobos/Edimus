using Mediator;

namespace Products.Core.Features.GetProductImage;
public class GetProductImageRequest : IRequest<GetProductImageResponse>
{
    public int Id { get; set; }
}
