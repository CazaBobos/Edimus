using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sectors.Core.Abstractions;
using Sectors.Core.Extensions;
using Sectors.Core.Model;
using Shared.Core.Extensions;

namespace Sectors.Core.Features.GetManySectors;

public class GetSectorsRequestHandler : IRequestHandler<GetSectorsRequest, GetSectorsResponse>
{
    private readonly ISectorsRepository _sectorsRepository;
    private readonly IMapper _mapper;

    public GetSectorsRequestHandler(ISectorsRepository sectorsRepository, IMapper mapper)
    {
        _sectorsRepository = sectorsRepository;
        _mapper = mapper;
    }

    public async Task<GetSectorsResponse> Handle(GetSectorsRequest request, CancellationToken cancellationToken)
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
            Sectors = _mapper.Map<List<SectorModel>>(sectors)
        };
    }
}