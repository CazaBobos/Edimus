using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Entities;
using Shared.Core.Exceptions;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Tables.Core.Abstractions;
using Tables.Core.Model;

namespace Tables.Core.Features.RemoveTable;

public class RemoveTableRequestHandler : IRequestHandler<RemoveTableRequest, RemoveTableResponse>
{
    private readonly ITablesRepository _tablesRepository;

    public RemoveTableRequestHandler(ITablesRepository tablesRepository)
    {
        _tablesRepository = tablesRepository;
    }

    public async Task<RemoveTableResponse> Handle(RemoveTableRequest request, CancellationToken cancellationToken)
    {
        var table = await _tablesRepository.GetById(request.Id);

        if (table is null) throw new HttpNotFoundException();

        await _tablesRepository.Remove(table, cancellationToken);
    
        return new RemoveTableResponse();
    }
}