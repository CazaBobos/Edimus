using AuditLogs.Core.Extensions;
using AuditLogs.Core.Model;
using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;
using AuditLogs.Core.Abstractions;
using Shared.Core.Extensions;

namespace AuditLogs.Core.Features.GetAuditLogs;

public class GetAuditLogsRequestHandler : IRequestHandler<GetAuditLogsRequest, GetAuditLogsResponse>
{
    private readonly IAuditLogsRepository _repository;

    public GetAuditLogsRequestHandler(IAuditLogsRepository repository)
    {
        _repository = repository;
    }

    public async ValueTask<GetAuditLogsResponse> Handle(GetAuditLogsRequest request, CancellationToken cancellationToken)
    {
        var query = _repository.AsQueryable()
            .WhereDateFrom(request.DateFrom)
            .WhereDateTo(request.DateTo)
            .WhereEntityType(request.EntityType)
            .WhereUsername(request.Username)
            .WhereOperation(request.Operation);

        var count = await query.CountAsync(cancellationToken);

        var logs = await query
            .OrderByDescending(l => l.DateTime)
            .Paginate(request.Limit, request.Page)
            .Include(l => l.Changes)
            .ToListAsync(cancellationToken);

        return new GetAuditLogsResponse
        {
            Count = count,
            Limit = request.Limit,
            Page = request.Page,
            AuditLogs = logs.Adapt<List<AuditLogModel>>(),
        };
    }
}
