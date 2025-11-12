using Dawn;
using Shared.Core.Domain;

namespace Shared.Core.Entities;

public class Establishment : Entity<int>
{
    public override int Id { get; protected set; }
    public string Name { get; protected set; } = string.Empty;
    public int CompanyId { get; protected set; }
    public virtual Company? Company { get; protected set; }
    public virtual List<Layout>? Layouts { get; protected set; }

    protected Establishment() { }
    public Establishment(int companyId, string name)
    {
        CompanyId = Guard.Argument(() => companyId).Positive();
        Name = Guard.Argument(() => name).NotNull().NotEmpty();
        Enabled = true;
    }
}
