using Mediator;
using Shared.Core.Exceptions;
using Tables.Core.Abstractions;

namespace Tables.Core.Features.UpdateTable;

public class UpdateTableRequestHandler : IRequestHandler<UpdateTableRequest, UpdateTableResponse>
{
    private readonly ITablesRepository _tablesRepository;
    private readonly ITableNotifier _notifier;

    public UpdateTableRequestHandler(ITablesRepository tablesRepository, ITableNotifier notifier)
    {
        _tablesRepository = tablesRepository;
        _notifier = notifier;
    }

    public async ValueTask<UpdateTableResponse> Handle(UpdateTableRequest request, CancellationToken cancellationToken)
    {
        var table = await _tablesRepository.GetById(request.Id, cancellationToken);

        if(table is null) throw new HttpNotFoundException();

        table.Update(
            request.Status,
            request.PositionX,
            request.PositionY,
            request.Surface?.Select(c => (c.X, c.Y)).ToList(),
            request.Orders?.Select(r => (r.ProductId, r.Amount)).ToList()
        );

        await _tablesRepository.Update(table, cancellationToken);

        if (request.Status is not null)
            await _notifier.NotifyStatusChanged(table.Id, table.LayoutId, table.Status, cancellationToken);

        return new UpdateTableResponse();
    }
}
