using Mediator;
using Shared.Core.Entities;

namespace Ingredients.Core.Features.UpdateIngredient;

public class UpdateIngredientRequest : IRequest<UpdateIngredientResponse>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal? Stock { get; set; }
    public int? Alert { get; set; }
    public MeasurementUnit? Unit { get; set; }
}