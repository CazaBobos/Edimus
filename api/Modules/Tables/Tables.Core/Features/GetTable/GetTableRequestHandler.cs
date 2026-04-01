using Mapster;
using Mediator;
using Shared.Core.Exceptions;
using Tables.Core.Abstractions;
using Tables.Core.Model;

namespace Tables.Core.Features.GetTable;

public class GetTableRequestHandler : IRequestHandler<GetTableRequest, GetTableResponse>
{
    private readonly ITablesRepository _tablesRepository;

    public GetTableRequestHandler(ITablesRepository tablesRepository)
    {
        _tablesRepository = tablesRepository;
    }

    public async ValueTask<GetTableResponse> Handle(GetTableRequest request, CancellationToken cancellationToken)
    {
        var table = await _tablesRepository.GetById(request.Id, cancellationToken);

        if (table is null) throw new HttpNotFoundException();

        return new GetTableResponse
        {
            Table = table.Adapt<TableModel>(),
        };
    }
}
