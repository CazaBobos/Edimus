using MediatR;
using Shared.Core.Abstractions;

namespace Products.Core.Features.CreateProduct;
public class UpdateProductRequest : IRequest<UpdateProductResponse>
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public int? CategoryId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public IUserRecord? User { get; set; }
}