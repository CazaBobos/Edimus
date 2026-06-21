namespace AuditLogs.Core.Model;

public class AuditLogChangeModel
{
    public string PropertyName { get; set; } = string.Empty;
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
}
