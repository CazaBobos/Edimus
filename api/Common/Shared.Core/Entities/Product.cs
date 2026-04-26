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
    public virtual List<Tag> Tags { get; protected set; } = [];
    public virtual List<Consumption> Consumptions { get; protected set; } = [];

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

    public void Update(int? parentId, int? categoryId, decimal? price, string? name, string? description, List<(int, decimal)>? consumptions)
    {
        Guard.Operation(Enabled == true, "A product cannot be modified when it's not active. Restore it and try again.");
        Guard.Operation(parentId == null || categoryId == null, "A product can either have a parent, or a category");

        if (parentId is not null && parentId != ParentId)
        {
            ParentId = (int)Guard.Argument(() => parentId).Positive();
            CategoryId = null;
        }
        if (categoryId is not null && categoryId != CategoryId)
        {
            CategoryId = (int)Guard.Argument(() => categoryId).Positive();
            ParentId = null;
        }
        if (name is not null && name != Name)
            Name = ValidateName(name);
        if (description is not null && description != Description)
            Description = Guard.Argument(() => description).NotNull().MaxLength(128);
        if (price is not null && price != Price)
            Price = Guard.Argument(() => (decimal)price).NotNegative();
        if (consumptions is not null)
        {
            Guard.Argument(() => consumptions).Require(x => x.All(s => s.Item1 > 0 && s.Item2 > 0));
            Consumptions.Clear();
            var newConsumptions = consumptions.Select(c => new Consumption(productId: Id, ingredientId: c.Item1, amount: c.Item2));
            Consumptions.AddRange(newConsumptions);
        }
    }

    public void UpdateImage(byte[]? blob)
    {
        if (blob is null)
        {
            Image = null;
            return;
        }

        Guard.Argument(() => blob).Require(x => x.Length <= 500_000, _ => "Image exceeds maximum allowed size of 500KB");

        if (Image is not null)
            Image.Update(blob);
        else
            Image = new Image(Id, blob);
    }

    private static string ValidateName(string name) => Guard.Argument(() => name).NotNull().NotEmpty().NotWhiteSpace().DoesNotContain("  ");
}
