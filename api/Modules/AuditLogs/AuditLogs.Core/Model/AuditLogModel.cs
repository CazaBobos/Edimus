using Shared.Core.Domain;

namespace AuditLogs.Core.Model;

public class AuditLogModel
{
    public long Id { get; set; }
    public string EntityType { get; set; } = string.Empty;
    public string EntityId { get; set; } = string.Empty;
    public AuditOperation Operation { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public DateTime DateTime { get; set; }
    public List<AuditLogChangeModel> Changes { get; set; } = [];
}