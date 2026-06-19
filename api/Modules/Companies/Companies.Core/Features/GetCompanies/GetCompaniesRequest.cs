using Mediator;

namespace Companies.Core.Features.GetCompanies;

public class GetCompaniesRequest : IRequest<GetCompaniesResponse>
{
    public int? Limit { get; set; }
    public int? Page { get; set; }
    public string? Name { get; set; }
    public string? Slogan { get; set; }
    public string? Slug { get; set; }
    public bool? Enabled { get; set; }
}