using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Entities;
using Shared.Core.Exceptions;
using Tables.Core.Abstractions;
using Tables.Core.Model;

namespace Tables.Core.Features.LinkTable;

public class LinkTableRequestHandler : IRequestHandler<LinkTableRequest, LinkTableResponse>
{
    private readonly ITablesRepository _tablesRepository;
    private readonly IMapper _mapper;

    public LinkTableRequestHandler(ITablesRepository tablesRepository, IMapper mapper)
    {
        _tablesRepository = tablesRepository;
        _mapper = mapper;
    }

    public async Task<LinkTableResponse> Handle(LinkTableRequest request, CancellationToken cancellationToken)
    {
        var table = await _tablesRepository.FindOne(x => x.QrId == request.TableId);

        if (table is null) throw new HttpNotFoundException();

        table.Update(status: TableStatus.Occupied);

        await _tablesRepository.Update(table, cancellationToken);

        return new LinkTableResponse
        {
            Table = _mapper.Map<TableModel>(table),
        };
    }
}