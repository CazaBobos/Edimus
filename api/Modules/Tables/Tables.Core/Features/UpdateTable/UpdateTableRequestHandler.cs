using Mediator;
using Shared.Core.Entities;
using Shared.Core.Exceptions;
using Shared.Core.Notifications;
using Tables.Core.Abstractions;

namespace Tables.Core.Features.UpdateTable;

public class UpdateTableRequestHandler : IRequestHandler<UpdateTableRequest, UpdateTableResponse>
{
    private readonly IMediator _mediator;
    private readonly ITablesRepository _tablesRepository;

    public UpdateTableRequestHandler(IMediator mediator, ITablesRepository tablesRepository)
    {
        _mediator = mediator;
        _tablesRepository = tablesRepository;
    }

    public async ValueTask<UpdateTableResponse> Handle(UpdateTableRequest request, CancellationToken cancellationToken)
    {
        var table = await _tablesRepository.GetById(request.Id, cancellationToken);

        if(table is null) throw new HttpNotFoundException();

        var oldOrders = table.Orders
            .ToDictionary(o => o.ProductId, o => o.Amount);

        table.Update(
            request.Status,
            request.PositionX,
            request.PositionY,
            request.Surface?.Select(c => (c.X, c.Y)).ToList(),
            request.Orders?.Select(r => (r.ProductId, r.Amount)).ToList()
        );

        await _tablesRepository.Update(table, cancellationToken);

        if (request.Orders is not null && request.Status != TableStatus.Free)
        {
            var delta = request.Orders
                .Select(o => (
                    o.ProductId,
                    Amount: o.Amount - oldOrders.GetValueOrDefault(o.ProductId, 0)
                ))
                .Where(d => d.Amount > 0)
                .ToList();

            await _mediator.Publish(new OrdersConfirmedNotification { Delta = delta }, cancellationToken);
        }

        return new UpdateTableResponse();
    }
}
