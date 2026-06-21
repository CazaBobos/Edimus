using Mediator;
using Shared.Core.Domain;

namespace AuditLogs.Core.Features.GetAuditLogs;

public class GetAuditLogsRequest : IRequest<GetAuditLogsResponse>
{
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public string? EntityType { get; set; }
    public string? Username { get; set; }
    public AuditOperation? Operation { get; set; }
    public int? Limit { get; set; }
    public int? Page { get; set; }
}
