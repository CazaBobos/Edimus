using Mediator;
using Shared.Core.Abstractions;

namespace Products.Core.Features.UpdateProductImage;

public class UpdateProductImageRequest : IRequest<UpdateProductImageResponse>
{
    public int Id { get; set; }
    public byte[]? Image { get; set; }
    public IUserRecord? User { get; set; }
}
