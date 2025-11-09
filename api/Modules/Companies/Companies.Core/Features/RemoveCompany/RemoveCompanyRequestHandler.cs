using AutoMapper;
using Companies.Core.Abstractions;
using Companies.Core.Model;
using MediatR;
using Shared.Core.Entities;
using Shared.Core.Exceptions;

namespace Companies.Core.Features.RemoveCompany;

public class RemoveCompanyRequestHandler : IRequestHandler<RemoveCompanyRequest, RemoveCompanyResponse>
{
    private readonly ICompaniesRepository _companiesRepository;

    public RemoveCompanyRequestHandler(ICompaniesRepository companiesRepository)
    {
        _companiesRepository = companiesRepository;
    }

    public async Task<RemoveCompanyResponse> Handle(RemoveCompanyRequest request, CancellationToken cancellationToken)
    {
        var company = await _companiesRepository.GetById(request.Id, cancellationToken);

        if (company is null) throw new HttpNotFoundException();

        company.Remove();

        await _companiesRepository.Update(company, cancellationToken);

        return new RemoveCompanyResponse();
    }
}