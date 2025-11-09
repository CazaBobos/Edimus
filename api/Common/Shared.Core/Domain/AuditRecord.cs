using Dawn;
using Shared.Core.Abstractions;
using Shared.Core.Extensions;

namespace Shared.Core.Domain;
public class AuditRecord : ValueObject
{
    public long UserId { get; protected set; }
    public string Username { get; protected set; }
    public DateTime DateTime { get; protected set; }
    public AuditOperation Operation { get; protected set; }
    public List<string>? AffectedMembers { get; set; }

    public AuditRecord(IUserRecord user, AuditOperation operation, List<string>? affectedMembers) 
        : this(user, DateTime.UtcNow, operation, affectedMembers)
    {
    }

    public AuditRecord(IUserRecord user, DateTime dateTime, AuditOperation operation, List<string>? affectedMembers)
    {
        Guard.Argument(() => user).NotNull();

        UserId = user.Id;
        Username = user.Username;
        DateTime = Guard.Argument(() => dateTime).ValidSqlDate().NotFuture();
        Operation = operation;
        AffectedMembers = affectedMembers;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return UserId;
        yield return Username;
        yield return DateTime;
        yield return Operation;
    }
}