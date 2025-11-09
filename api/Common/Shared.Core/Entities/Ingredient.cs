using Dawn;
using Shared.Core.Domain;

namespace Shared.Core.Entities;
public class Ingredient : Entity<int>
{
    public override int Id { get; protected set; }
    public string Name{ get; protected set; } = string.Empty;
    public int Stock { get; protected set; }
    public int Alert { get; protected set; }
    public MeasurementUnit? Unit{ get; protected set; }

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

    public void Update(string name, int stock, int alert, MeasurementUnit unit)
    {
        Guard.Operation(Enabled == true, "A device cannot be modified when it's not active. Restore it and try again.");

        Name = Guard.Argument(() => name)
            .NotNull()
            .MaxLength(32);
        Stock = Guard.Argument(() => stock).Positive();
        Alert = Guard.Argument(() => alert).Positive();
        Unit = unit;
    }
}
