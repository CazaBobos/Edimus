using Dawn;
using Shared.Core.Domain;

namespace Shared.Core.Entities;
public class Tag : Entity<int>
{
    public int CompanyId { get; protected set; }
    public virtual Company? Company { get; protected set; }
    public string Name { get; protected set; } = string.Empty;
    public virtual List<Product> Products { get; protected set; } = [];

    protected Tag() { }
    public Tag(int companyId, string name)
    {
        CompanyId = Guard.Argument(() => companyId).Positive();
        Name = name;
        Enabled = true;
    }

    public void Update(string name)
    {
        Name = Guard.Argument(() => name).NotNull().NotEmpty().MaxLength(32);
    }
}