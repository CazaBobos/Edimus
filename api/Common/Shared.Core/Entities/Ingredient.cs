using Dawn;
using Shared.Core.Domain;

namespace Shared.Core.Entities;
public class Ingredient : Entity<int>
{
    public override int Id { get; protected set; }
    public string Name { get; protected set; } = string.Empty;
    public int Stock { get; protected set; }
    public int Alert { get; protected set; }
    public MeasurementUnit Unit { get; protected set; }
    public virtual List<Consumption>? Consumptions { get; protected set; }

    protected Ingredient() { }
    public Ingredient(string name, int stock, int alert, MeasurementUnit unit)
    {
        Name = Guard.Argument(() => name)
            .NotNull()
            .MaxLength(32);
        Stock = Guard.Argument(() => stock).Positive();
        Alert = Guard.Argument(() => alert).Positive();
        Unit = unit;
        Enabled = true;
    }

    public void Update(string? name, int? stock, int? alert, MeasurementUnit? unit)
    {
        Guard.Operation(Enabled == true, "An ingredient cannot be modified when it's not active. Restore it and try again.");

        var affectedMembers = new List<string>();

        if (name is not null && name != Name)
        {
            Name = Guard.Argument(() => name)
                .NotNull()
                .MaxLength(32);
            affectedMembers.Add(nameof(Name));
        }
        if (stock is not null && stock != Stock)
        {
            Stock = Guard.Argument(() => (int)stock).Positive();
            affectedMembers.Add(nameof(Stock));
        }
        if (alert is not null && alert != Alert)
        {
            Alert = Guard.Argument(() => (int)alert).Positive();
            affectedMembers.Add(nameof(Alert));
        }
        if (unit is not null && unit != Unit)
        {
            Unit = (MeasurementUnit)unit;
            affectedMembers.Add(nameof(Unit));
        }
    }
}
