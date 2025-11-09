using MediatR;
using Shared.Core.Abstractions;

namespace Products.Core.Features.GetOneProduct;
public class GetOneProductRequest : IRequest<GetOneProductResponse>
{
    public int Id { get; set; }
    public IUserRecord? User { get; set; }
}
