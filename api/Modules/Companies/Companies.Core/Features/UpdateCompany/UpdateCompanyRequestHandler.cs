using Companies.Core.Abstractions;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Exceptions;

namespace Companies.Core.Features.UpdateCompany;

public class UpdateCompanyRequestHandler : IRequestHandler<UpdateCompanyRequest, UpdateCompanyResponse>
{
    private readonly ICompaniesRepository _companiesRepository;

    public UpdateCompanyRequestHandler(ICompaniesRepository companiesRepository)
    {
        _companiesRepository = companiesRepository;
    }

    public async ValueTask<UpdateCompanyResponse> Handle(UpdateCompanyRequest request, CancellationToken cancellationToken)
    {
        var company = await _companiesRepository.GetById(request.Id, cancellationToken);

        if (company is null) throw new HttpNotFoundException();

        company.Update(request.Name, request.Slogan);

        var existingCompany = await _companiesRepository.AsQueryable()
            .Where(x => x.Id != company.Id && x.Name == company.Name)
            .FirstOrDefaultAsync(cancellationToken);

        if (existingCompany is not null)
            throw new InvalidOperationException("The company name already exists");

        await _companiesRepository.Update(company, cancellationToken);

        return new UpdateCompanyResponse();
    }
}