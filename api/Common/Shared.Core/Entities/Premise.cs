using Dawn;
using Shared.Core.Domain;

namespace Shared.Core.Entities;

public class Premise : Entity<int>
{
    public override int Id { get; protected set; }
    public string Name { get; protected set; } = string.Empty;
    public int CompanyId { get; protected set; }
    public virtual Company? Company { get; protected set; }
    public virtual List<Layout>? Layouts { get; protected set; }

    protected Premise() { }
    public Premise(int companyId, string name)
    {
        CompanyId = Guard.Argument(() => companyId).Positive();
        Name = Guard.Argument(() => name).NotNull().NotEmpty();
        Enabled = true;
    }
}
