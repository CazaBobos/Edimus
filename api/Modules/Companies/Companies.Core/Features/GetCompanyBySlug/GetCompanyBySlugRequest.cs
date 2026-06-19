using Mediator;
using Companies.Core.Features.GetCompany;

namespace Companies.Core.Features.GetCompanyBySlug;

public class GetCompanyBySlugRequest : IRequest<GetCompanyResponse>
{
    public string Slug { get; set; } = string.Empty;
}
