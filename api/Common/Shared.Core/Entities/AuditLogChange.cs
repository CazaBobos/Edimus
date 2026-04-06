namespace Shared.Core.Entities;

public class AuditLogChange
{
    public long Id { get; private set; }
    public long AuditLogId { get; private set; }
    public string PropertyName { get; private set; } = string.Empty;
    public string? OldValue { get; private set; }
    public string? NewValue { get; private set; }

    protected AuditLogChange() { }

    public AuditLogChange(string propertyName, string? oldValue, string? newValue)
    {
        PropertyName = propertyName;
        OldValue = oldValue;
        NewValue = newValue;
    }
}
