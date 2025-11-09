using Companies.Core.Model;

namespace Companies.Core.Features.GetCompany;
public class GetCompanyResponse
{
    public CompanyModel Company { get; set; } = new();
}