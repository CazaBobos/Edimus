using MediatR;
using Shared.Core.Abstractions;

namespace Products.Core.Features.CreateProduct;
public class CreateProductRequest : IRequest<CreateProductResponse>
{
    public int? ParentId { get; set; }
    public int? CategoryId { get; set; }
    public decimal Price { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IUserRecord? User { get; set; }
}