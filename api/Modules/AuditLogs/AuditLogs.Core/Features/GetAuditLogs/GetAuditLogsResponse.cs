using AuditLogs.Core.Model;

namespace AuditLogs.Core.Features.GetAuditLogs;

public class GetAuditLogsResponse
{
    public int Count { get; set; }
    public int? Limit { get; set; }
    public int? Page { get; set; }
    public List<AuditLogModel> AuditLogs { get; set; } = [];
}
