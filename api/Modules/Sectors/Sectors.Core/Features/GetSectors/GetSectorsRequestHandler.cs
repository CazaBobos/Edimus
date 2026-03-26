using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Sectors.Core.Abstractions;
using Sectors.Core.Extensions;
using Sectors.Core.Model;
using Shared.Core.Extensions;

namespace Sectors.Core.Features.GetManySectors;

public class GetSectorsRequestHandler : IRequestHandler<GetSectorsRequest, GetSectorsResponse>
{
    private readonly ISectorsRepository _sectorsRepository;

    public GetSectorsRequestHandler(ISectorsRepository sectorsRepository)
    {
        _sectorsRepository = sectorsRepository;
    }

    public async ValueTask<GetSectorsResponse> Handle(GetSectorsRequest request, CancellationToken cancellationToken)
    {
        var query = _sectorsRepository.AsQueryable()
            .WhereLayout(request.LayoutId)
            .WhereName(request.Name)
            .WhereEnabled(request.Enabled);

        var sectors = await query
            .Paginate(request.Limit, request.Page)
            .ToListAsync(cancellationToken);

        return new GetSectorsResponse
        {
            Count = query.Count(),
            Limit = request.Limit,
            Page = request.Page,
            Sectors = sectors.Adapt<List<SectorModel>>()
        };
    }
}