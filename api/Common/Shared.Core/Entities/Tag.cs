using Dawn;
using Shared.Core.Domain;

namespace Shared.Core.Entities;
public class Tag : Entity<int>
{
    public string Name { get; protected set; } = string.Empty;
    public virtual List<Product>? Products { get; protected set; }

    protected Tag() { }
    public Tag(string name)
    {
        Name = name;
        Enabled = true;
    }

    public void Update(string name)
    {
        Name = Guard.Argument(() => name).NotNull().NotEmpty().MaxLength(32);
    }
}