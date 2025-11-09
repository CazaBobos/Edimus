using Companies.Core.Model;

namespace Companies.Core.Features.GetCompanies;
public class GetCompaniesResponse
{
    public int Count { get; set; }
    public int? Limit { get; set; }
    public int? Page { get; set; }
    public List<CompanyModel> Companies { get; set; } = new();
}