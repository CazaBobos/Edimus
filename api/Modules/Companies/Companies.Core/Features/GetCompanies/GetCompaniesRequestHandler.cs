using AutoMapper;
using Companies.Core.Abstractions;
using Companies.Core.Extensions;
using Companies.Core.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Extensions;

namespace Companies.Core.Features.GetCompanies;

public class GetCompaniesRequestHandler : IRequestHandler<GetCompaniesRequest, GetCompaniesResponse>
{
    private readonly ICompaniesRepository _companiesRepository;
    private readonly IMapper _mapper;

    public GetCompaniesRequestHandler(ICompaniesRepository companiesRepository, IMapper mapper)
    {
        _companiesRepository = companiesRepository;
        _mapper = mapper;
    }

    public async Task<GetCompaniesResponse> Handle(GetCompaniesRequest request, CancellationToken cancellationToken)
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
            Companies = _mapper.Map<List<CompanyModel>>(companies),
        };
    }
}