using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Extensions;
using Tables.Core.Abstractions;
using Tables.Core.Extensions;
using Tables.Core.Model;

namespace Tables.Core.Features.GetManyTables;

public class GetTablesRequestHandler : IRequestHandler<GetTablesRequest, GetTablesResponse>
{
    private readonly ITablesRepository _tablesRepository;

    public GetTablesRequestHandler(ITablesRepository tablesRepository)
    {
        _tablesRepository = tablesRepository;
    }

    public async ValueTask<GetTablesResponse> Handle(GetTablesRequest request, CancellationToken cancellationToken)
    {
        var query = _tablesRepository.AsQueryable()
            .WhereLayout(request.LayoutId)
            .WhereStatus(request.Status)
            .WhereEnabled(request.Enabled);

        var tables = await query
            .Paginate(request.Limit, request.Page)
            .ToListAsync(cancellationToken);

        return new GetTablesResponse
        {
            Count = query.Count(),
            Limit = request.Limit,
            Page = request.Page,
            Tables = tables.Adapt<List<TableModel>>()
        };
    }
}