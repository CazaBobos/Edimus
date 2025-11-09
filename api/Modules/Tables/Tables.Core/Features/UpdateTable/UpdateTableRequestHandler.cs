using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Entities;
using Shared.Core.Exceptions;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Tables.Core.Abstractions;
using Tables.Core.Model;

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
            request.Surface
        );

        await _tablesRepository.Update(table, cancellationToken);
    
        return new UpdateTableResponse();
    }
}