using Dawn;
using Shared.Core.Domain;

namespace Shared.Core.Entities;

public class Category : Entity<int>
{
    public override int Id { get; protected set; }
    public string Name { get; protected set; } = string.Empty;
    public int CompanyId { get; protected set; }
    public virtual Company? Company { get; protected set; } 
    public virtual List<Product>? Products { get; protected set; }

    protected Category() { }
    public Category(int companyId, string name)
    {
        CompanyId = Guard.Argument(() => companyId).Positive();
        Name = ValidateName(name);
        Enabled = true;
    }

    public void Update(string name)
    {
        var affectedMembers = new List<string>();

        if (name is not null && name != Name)
        {
            Name = ValidateName(name);
            affectedMembers.Add(nameof(Name));
        }

        //if (affectedMembers.Count != 0) AddHistory(user, AuditOperation.Updated, affectedMembers);
    }

    private string ValidateName(string name) => Guard.Argument(() => name).NotNull().NotEmpty().NotWhiteSpace().MaxLength(32);
}