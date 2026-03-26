using Mapster;
using Companies.Core.Abstractions;
using Companies.Core.Model;
using Mediator;

namespace Companies.Core.Features.GetCompany;

public class GetCompanyRequestHandler : IRequestHandler<GetCompanyRequest, GetCompanyResponse>
{
    private readonly ICompaniesRepository _companiesRepository;

    public GetCompanyRequestHandler(ICompaniesRepository companiesRepository)
    {
        _companiesRepository = companiesRepository;
    }

    public async ValueTask<GetCompanyResponse> Handle(GetCompanyRequest request, CancellationToken cancellationToken)
    {
        var company = await _companiesRepository.GetById(request.Id, cancellationToken);

        return new GetCompanyResponse
        {
            Company = company.Adapt<CompanyModel>()
        };
    }
}