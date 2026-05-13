using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Premises.Core.Extensions;
using Premises.Core.Model;
using Shared.Core.Extensions;
using Premises.Core.Abstractions;

namespace Premises.Core.Features.GetPremises;

public class GetPremisesRequestHandler : IRequestHandler<GetPremisesRequest, GetPremisesResponse>
{
    private readonly IPremisesRepository _premisesRepository;

    public GetPremisesRequestHandler(IPremisesRepository premisesRepository)
    {
        _premisesRepository = premisesRepository;
    }

    public async ValueTask<GetPremisesResponse> Handle(GetPremisesRequest request, CancellationToken cancellationToken)
    {
        var query = _premisesRepository.AsQueryable()
            .WhereCompany(request.CompanyId)
            .WhereEnabled(request.Enabled);

        var count = await query.CountAsync(cancellationToken);

        var premises = await query
            .Paginate(request.Limit, request.Page)
            .OrderBy(x => x.Id)
            .ToListAsync(cancellationToken);

        return new GetPremisesResponse
        {
            Count = count,
            Limit = request.Limit,
            Page = request.Page,
            Premises = premises.Adapt<List<PremiseModel>>()
        };
    }
}
