using Shared.Core.Domain;

namespace Shared.Core.Entities;

public class AuditLog
{
    public long Id { get; private set; }
    public string EntityId { get; private set; } = string.Empty;
    public string EntityType { get; private set; } = string.Empty;
    public AuditOperation Operation { get; private set; }
    public int UserId { get; private set; }
    public string Username { get; private set; } = string.Empty;
    public DateTime DateTime { get; private set; }
    public virtual List<AuditLogChange> Changes { get; private set; } = [];

    protected AuditLog() { }

    public AuditLog(
        string entityType,
        string entityId,
        AuditOperation operation,
        int userId,
        string username,
        DateTime dateTime,
        List<AuditLogChange>? changes = null)
    {
        EntityType = entityType;
        EntityId = entityId;
        Operation = operation;
        UserId = userId;
        Username = username;
        DateTime = dateTime;
        Changes = changes ?? [];
    }
}
