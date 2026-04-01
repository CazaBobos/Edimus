using Mapster;
using Mediator;
using Shared.Core.Exceptions;
using Tables.Core.Abstractions;
using Tables.Core.Model;

namespace Tables.Core.Features.LinkTable;

public class LinkTableRequestHandler : IRequestHandler<LinkTableRequest, LinkTableResponse>
{
    private readonly ITablesRepository _tablesRepository;
    private readonly ITableNotifier _notifier;

    public LinkTableRequestHandler(ITablesRepository tablesRepository, ITableNotifier notifier)
    {
        _tablesRepository = tablesRepository;
        _notifier = notifier;
    }

    public async ValueTask<LinkTableResponse> Handle(LinkTableRequest request, CancellationToken cancellationToken)
    {
        var table = await _tablesRepository.FindOne(x => x.QrId == request.TableId);

        if (table is null) throw new HttpNotFoundException();

        table.Arrive();

        await _tablesRepository.Update(table, cancellationToken);

        await _notifier.NotifyStatusChanged(table.Id, table.LayoutId, table.Status, cancellationToken);

        return new LinkTableResponse
        {
            Table = table.Adapt<TableModel>(),
        };
    }
}