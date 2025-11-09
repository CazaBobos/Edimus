using MediatR;

namespace Categories.Core.Features.GetManyCategories;

public class GetCategoriesRequest : IRequest<GetCategoriesResponse>
{
    public int? Limit { get; set; }
    public int? Page { get; set; }
    public long? CompanyId { get; set; }
    public string? Name { get; set; }
    public bool? Enabled { get; set; }
}