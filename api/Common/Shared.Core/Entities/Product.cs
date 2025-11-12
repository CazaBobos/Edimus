using Dawn;
using Shared.Core.Domain;

namespace Shared.Core.Entities;
public class Product : AggregateRoot<int>
{
    public override int Id { get; protected set; }
    public int? ParentId { get; protected set; }
    public int? CategoryId { get; protected set; }
    public virtual Category? Category { get; protected set; }
    public string Name { get; protected set; } = string.Empty;
    public string Description { get; protected set; } = string.Empty;
    public decimal Price { get; protected set; }
    public int? ImageId { get; protected set; }
    public virtual Image? Image { get; protected set; }
    public virtual List<Tag>? Tags { get; protected set; }
    public virtual List<Consumption>? Consumptions { get; protected set; }

    protected Product() { }
    public Product(int? parentId, int? categoryId, decimal price, string name, string description = "")
    {
        Guard.Operation(parentId != null ^ categoryId != null, "A product can either have a parent, or a category");

        ParentId = Guard.Argument(() => parentId).Positive();
        CategoryId = Guard.Argument(() => categoryId).Positive();
        Name = ValidateName(name);
        Description = Guard.Argument(() => description)
            .NotNull()
            .MaxLength(250);
        Price = Guard.Argument(() => price).NotNegative();
        Enabled = true;
    }

    public void Update(int? parentId, int? categoryId, decimal? price, string? name, string? description)
    {
        Guard.Operation(Enabled == true, "A product cannot be modified when it's not active. Restore it and try again.");

        var affectedMembers = new List<string>();

        if (parentId is not null && parentId != ParentId)
        {
            ParentId = parentId;
            affectedMembers.Add(nameof(ParentId));
        }
        if (name is not null && name != Name)
        {
            Name = ValidateName(name);
            affectedMembers.Add(nameof(Name));
        }
        if (description is not null && description != Description)
        {
            Description = Guard.Argument(() => description)
                .NotNull()
                .MaxLength(128);
            affectedMembers.Add(nameof(Description));
        }
        if (categoryId is not null && categoryId != CategoryId)
        {
            CategoryId = Guard.Argument(() => (int)categoryId).Positive();
            affectedMembers.Add(nameof(CategoryId));
        }
        if (price is not null && price != Price)
        {
            Price = Guard.Argument(() => (decimal)price).NotNegative();
            affectedMembers.Add(nameof(Price));
        }

        //if (affectedMembers.Count != 0) AddHistory(user, AuditOperation.Updated, affectedMembers);
    }

    private string ValidateName(string name) => Guard.Argument(() => name).NotNull().NotEmpty().NotWhiteSpace().DoesNotContain("  ");
}
