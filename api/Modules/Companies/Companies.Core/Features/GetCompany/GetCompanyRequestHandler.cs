using AutoMapper;
using Companies.Core.Abstractions;
using Companies.Core.Model;
using MediatR;

namespace Companies.Core.Features.GetCompany;

public class GetCompanyRequestHandler : IRequestHandler<GetCompanyRequest, GetCompanyResponse>
{
    private readonly ICompaniesRepository _companiesRepository;
    private readonly IMapper _mapper;

    public GetCompanyRequestHandler(ICompaniesRepository companiesRepository, IMapper mapper)
    {
        _companiesRepository = companiesRepository;
        _mapper = mapper;
    }

    public async Task<GetCompanyResponse> Handle(GetCompanyRequest request, CancellationToken cancellationToken)
    {
        var company = await _companiesRepository.GetById(request.Id, cancellationToken);

        return new GetCompanyResponse
        {
            Company = _mapper.Map<CompanyModel>(company)
        };
    }
}