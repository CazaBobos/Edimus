using AutoMapper;
using Tables.Core.Abstractions;
using Tables.Core.Extensions;
using Tables.Core.Model;
using MediatR;
using Shared.Core.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Tables.Core.Features.GetManyTables;

public class GetTablesRequestHandler : IRequestHandler<GetTablesRequest, GetTablesResponse>
{
    private readonly ITablesRepository _tablesRepository;
    private readonly IMapper _mapper;

    public GetTablesRequestHandler(ITablesRepository tablesRepository, IMapper mapper)
    {
        _tablesRepository = tablesRepository;
        _mapper = mapper;
    }

    public async Task<GetTablesResponse> Handle(GetTablesRequest request, CancellationToken cancellationToken)
    {
        var query = _tablesRepository.AsQueryable()
            .WhereEnabled(request.Enabled);

        var tables = await query
            .Paginate(request.Limit, request.Page)
            .ToListAsync(cancellationToken);

        return new GetTablesResponse
        {
            Count = query.Count(),
            Limit = request.Limit,
            Page = request.Page,
            Tables = _mapper.Map<List<TableModel>>(tables)
        };
    }
}