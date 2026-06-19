using Mapster;
using Companies.Core.Abstractions;
using Companies.Core.Features.GetCompany;
using Companies.Core.Model;
using Mediator;
using Shared.Core.Exceptions;

namespace Companies.Core.Features.GetCompanyBySlug;

public class GetCompanyBySlugRequestHandler : IRequestHandler<GetCompanyBySlugRequest, GetCompanyResponse>
{
    private readonly ICompaniesRepository _companiesRepository;

    public GetCompanyBySlugRequestHandler(ICompaniesRepository companiesRepository)
    {
        _companiesRepository = companiesRepository;
    }

    public async ValueTask<GetCompanyResponse> Handle(GetCompanyBySlugRequest request, CancellationToken cancellationToken)
    {
        var company = await _companiesRepository.FindOne(c => c.Slug == request.Slug && c.Enabled, cancellationToken);

        if (company is null) throw new HttpNotFoundException();

        return new GetCompanyResponse
        {
            Company = company.Adapt<CompanyModel>()
        };
    }
}
