using Shared.Core.Entities;

namespace Ingredients.Input;
public class CreateIngredientInput
{
    public int CompanyId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Stock { get; set; }
    public int Alert { get; set; }
    public MeasurementUnit Unit { get; set; }
}
