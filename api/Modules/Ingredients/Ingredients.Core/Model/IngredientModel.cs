using Shared.Core.Entities;

namespace Ingredients.Core.Model;
public class IngredientModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Stock { get; set; }
    public int Alert { get; set; }
    public MeasurementUnit Unit { get; set; }
}