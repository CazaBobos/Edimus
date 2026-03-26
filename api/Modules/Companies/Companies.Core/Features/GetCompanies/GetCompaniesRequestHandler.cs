using Mapster;
using Companies.Core.Abstractions;
using Companies.Core.Extensions;
using Companies.Core.Model;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Extensions;

namespace Companies.Core.Features.GetCompanies;

public class GetCompaniesRequestHandler : IRequestHandler<GetCompaniesRequest, GetCompaniesResponse>
{
    private readonly ICompaniesRepository _companiesRepository;

    public GetCompaniesRequestHandler(ICompaniesRepository companiesRepository)
    {
        _companiesRepository = companiesRepository;
    }

    public async ValueTask<GetCompaniesResponse> Handle(GetCompaniesRequest request, CancellationToken cancellationToken)
    {
        var query = _companiesRepository.AsQueryable()
            .WhereName(request.Name)
            .WhereSlogan(request.Slogan)
            .WhereAcronym(request.Acronym)
            .WhereEnabled(request.Enabled);

        var companies = await query
            .Paginate(request.Limit, request.Page)
            .ToListAsync(cancellationToken);

        return new GetCompaniesResponse
        {
            Count = query.Count(),
            Limit = request.Limit,
            Page = request.Page,
            Companies = companies.Adapt<List<CompanyModel>>(),
        };
    }
}