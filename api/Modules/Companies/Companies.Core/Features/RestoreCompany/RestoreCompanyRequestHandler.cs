using Companies.Core.Abstractions;
using MediatR;
using Shared.Core.Exceptions;

namespace Companies.Core.Features.RestoreCompany;

public class RestoreCompanyRequestHandler : IRequestHandler<RestoreCompanyRequest, RestoreCompanyResponse>
{
    private readonly ICompaniesRepository _companiesRepository;

    public RestoreCompanyRequestHandler(ICompaniesRepository companiesRepository)
    {
        _companiesRepository = companiesRepository;
    }

    public async Task<RestoreCompanyResponse> Handle(RestoreCompanyRequest request, CancellationToken cancellationToken)
    {
        var company = await _companiesRepository.GetById(request.Id, cancellationToken);

        if (company is null) throw new HttpNotFoundException();

        company.Restore();

        await _companiesRepository.Update(company, cancellationToken);

        return new RestoreCompanyResponse();
    }
}