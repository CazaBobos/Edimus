using MediatR;
using Shared.Core.Abstractions;
using Shared.Core.Entities;

namespace Ingredients.Core.Features.CreateIngredient;

public class CreateIngredientRequest : IRequest<CreateIngredientResponse>
{
    public string Name { get; set; } = string.Empty;
    public int Stock { get; set; }
    public int Alert { get; set; }
    public MeasurementUnit Unit { get; set; }
    public IUserRecord User { get; set; } = null!;
}