using MediatR;
using Shared.Core.Abstractions;

namespace Products.Core.Features.CreateProduct;
public class CreateProductRequest : IRequest<CreateProductResponse>
{
    public int? Parent { get; set; }
    public int? Category { get; set; }
    public decimal Price { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IUserRecord? User { get; set; }
}