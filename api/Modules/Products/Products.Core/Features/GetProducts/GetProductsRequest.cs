using MediatR;
using Shared.Core.Abstractions;

namespace Products.Core.Features.GetProducts;

public class GetProductsRequest : IRequest<GetProductsResponse>
{
    public int? Limit { get; set; }
    public int? Page { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<int>? Categories { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public List<int>? Tags { get; set; }
    public bool? Enabled { get; set; }
    public IUserRecord? User { get; set; }
}