using Mediator;

namespace Products.Core.Features.UpdateProductImage;

public class UpdateProductImageRequest : IRequest<UpdateProductImageResponse>
{
    public int Id { get; set; }
    public byte[]? Image { get; set; }
}
