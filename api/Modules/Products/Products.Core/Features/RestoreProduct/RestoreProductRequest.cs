using MediatR;
using Shared.Core.Abstractions;

namespace Products.Core.Features.RestoreProduct;
public class RestoreProductRequest : IRequest<RestoreProductResponse>
{
    public int Id { get; set; }
    public IUserRecord? User { get; set; }
}
