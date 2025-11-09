using MediatR;
using Shared.Core.Entities;
using Tables.Core.Abstractions;

namespace Tables.Core.Features.CreateTable;

public class CreateTableRequestHandler : IRequestHandler<CreateTableRequest, CreateTableResponse>
{
    private readonly ITablesRepository _tablesRepository;

    public CreateTableRequestHandler(ITablesRepository tablesRepository)
    {
        _tablesRepository = tablesRepository;
    }

    public async Task<CreateTableResponse> Handle(CreateTableRequest request, CancellationToken cancellationToken)
    {
        var table = new Table(
            request.LayoutId,
            request.PositionX,
            request.PositionY, 
            request.Surface
        );

        await _tablesRepository.Add(table, cancellationToken);

        return new CreateTableResponse
        {
            Id = table.Id
        };
    }
}