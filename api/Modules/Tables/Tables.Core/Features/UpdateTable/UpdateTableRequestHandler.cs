using Mediator;
using Shared.Core.Entities;
using Shared.Core.Exceptions;
using Tables.Core.Abstractions;

namespace Tables.Core.Features.UpdateTable;

public class UpdateTableRequestHandler : IRequestHandler<UpdateTableRequest, UpdateTableResponse>
{
    private readonly ITablesRepository _tablesRepository;
    private readonly ITableSessionRepository _sessionRepository;
    private readonly ITableNotifier _notifier;

    public UpdateTableRequestHandler(
        ITablesRepository tablesRepository,
        ITableSessionRepository sessionRepository,
        ITableNotifier notifier)
    {
        _tablesRepository = tablesRepository;
        _sessionRepository = sessionRepository;
        _notifier = notifier;
    }

    public async ValueTask<UpdateTableResponse> Handle(UpdateTableRequest request, CancellationToken cancellationToken)
    {
        var table = await _tablesRepository.GetById(request.Id, cancellationToken);

        if (table is null) throw new HttpNotFoundException();

        var previousStatus = table.Status;
        var arrivedAt = table.ArrivedAt;
        var calledAt = table.CalledAt;
        var now = DateTime.UtcNow;

        // Snapshot orders before update — they get cleared when status → Free
        var orderSnapshot = request.Status == TableStatus.Free
            ? table.Orders
                .Select(o => new SessionOrder(o.ProductId, o.Product?.Name ?? "", o.Product?.Price ?? 0, o.Amount))
                .ToList()
            : null;

        table.Update(
            request.Status,
            request.PositionX,
            request.PositionY,
            request.Surface?.Select(c => (c.X, c.Y)).ToList(),
            request.Orders?.Select(r => (r.ProductId, r.Amount)).ToList()
        );

        await _tablesRepository.Update(table, cancellationToken);

        if (request.Status is not null && request.Status != previousStatus)
        {
            switch (request.Status)
            {
                case TableStatus.Occupied when previousStatus is TableStatus.Free or TableStatus.Arrived:
                    var arrivalSeconds = arrivedAt.HasValue
                        ? (int)(now - arrivedAt.Value).TotalSeconds
                        : (int?)null;
                    await _sessionRepository.Add(
                        new TableSession(table.Id, now, arrivedAt, arrivalSeconds),
                        cancellationToken);
                    break;

                case TableStatus.Occupied when previousStatus == TableStatus.Calling:
                    var activeSession = await _sessionRepository.GetActiveForTable(table.Id, cancellationToken);
                    if (activeSession is not null && calledAt.HasValue)
                    {
                        activeSession.AddCallingTime((int)(now - calledAt.Value).TotalSeconds);
                        await _sessionRepository.Update(activeSession, cancellationToken);
                    }
                    break;

                case TableStatus.Free when orderSnapshot is not null:
                    var sessionToClose = await _sessionRepository.GetActiveForTable(table.Id, cancellationToken);
                    if (sessionToClose is not null)
                    {
                        sessionToClose.Close(now, orderSnapshot);
                        await _sessionRepository.Update(sessionToClose, cancellationToken);
                    }
                    break;
            }
        }

        if (request.Status is not null)
            await _notifier.NotifyStatusChanged(table.Id, table.LayoutId, table.Status, cancellationToken);

        return new UpdateTableResponse();
    }
}
