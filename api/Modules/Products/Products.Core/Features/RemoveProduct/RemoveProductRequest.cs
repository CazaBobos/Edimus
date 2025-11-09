using MediatR;
using Shared.Core.Abstractions;

namespace Products.Core.Features.RemoveProduct;
public class RemoveProductRequest : IRequest<RemoveProductResponse>
{
    public int Id { get; set; }
    public IUserRecord? User { get; set; }
}
