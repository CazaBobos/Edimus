using MediatR;
using Shared.Core.Exceptions;
using Tables.Core.Abstractions;

namespace Tables.Core.Features.UpdateTable;

public class UpdateTableRequestHandler : IRequestHandler<UpdateTableRequest, UpdateTableResponse>
{
    private readonly ITablesRepository _tablesRepository;

    public UpdateTableRequestHandler(ITablesRepository tablesRepository)
    {
        _tablesRepository = tablesRepository;
    }

    public async Task<UpdateTableResponse> Handle(UpdateTableRequest request, CancellationToken cancellationToken)
    {
        var table = await _tablesRepository.GetById(request.Id);

        if (table is null) throw new HttpNotFoundException();

        table.Update(
            request.Status,
            request.PositionX,
            request.PositionY,
            request.Surface?.Select(c => (c.X, c.Y)).ToList()
        );

        await _tablesRepository.Update(table, cancellationToken);

        return new UpdateTableResponse();
    }
}