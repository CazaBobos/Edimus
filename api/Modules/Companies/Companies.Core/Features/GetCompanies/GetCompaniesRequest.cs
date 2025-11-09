using MediatR;

namespace Companies.Core.Features.GetCompanies;

public class GetCompaniesRequest : IRequest<GetCompaniesResponse>
{
    public int? Limit { get; set; }
    public int? Page { get; set; }
    public string? Name { get; set; }
    public string? Slogan { get; set; }
    public string? Acronym { get; set; }
    public bool? Enabled { get; set; }
}