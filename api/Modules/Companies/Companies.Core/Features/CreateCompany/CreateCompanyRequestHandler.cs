using Companies.Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Entities;

namespace Companies.Core.Features.CreateCompany;

public class CreateCompanyRequestHandler : IRequestHandler<CreateCompanyRequest, CreateCompanyResponse>
{
    private readonly ICompaniesRepository _companiesRepository;

    public CreateCompanyRequestHandler(ICompaniesRepository companiesRepository)
    {
        _companiesRepository = companiesRepository;
    }

    public async Task<CreateCompanyResponse> Handle(CreateCompanyRequest request, CancellationToken cancellationToken)
    {
        var company = new Company(
            request.Name,
            request.Slogan,
            request.Acronym
        );

        var existingCompany = await _companiesRepository.AsQueryable()
            .Where(x => x.Id == company.Id || x.Name == company.Name)
            .FirstOrDefaultAsync(cancellationToken);

        if (existingCompany is not null)
            throw new InvalidOperationException("The company already exists");

        await _companiesRepository.Add(company, cancellationToken);

        return new CreateCompanyResponse
        {
            Id = company.Id
        };
    }
}