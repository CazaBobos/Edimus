using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Entities;
using Shared.Core.Exceptions;
using Tables.Core.Abstractions;
using Tables.Core.Model;

namespace Tables.Core.Features.LinkTable;

public class LinkTableRequestHandler : IRequestHandler<LinkTableRequest, LinkTableResponse>
{
    private readonly ITablesRepository _tablesRepository;

    public LinkTableRequestHandler(ITablesRepository tablesRepository)
    {
        _tablesRepository = tablesRepository;
    }

    public async ValueTask<LinkTableResponse> Handle(LinkTableRequest request, CancellationToken cancellationToken)
    {
        var table = await _tablesRepository.FindOne(x => x.QrId == request.TableId);

        if (table is null) throw new HttpNotFoundException();

        table.Update(status: TableStatus.Occupied);

        await _tablesRepository.Update(table, cancellationToken);

        return new LinkTableResponse
        {
            Table = table.Adapt<TableModel>(),
        };
    }
}