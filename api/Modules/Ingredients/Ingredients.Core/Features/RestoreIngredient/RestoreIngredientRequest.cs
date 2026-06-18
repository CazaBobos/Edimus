using Mediator;

namespace Ingredients.Core.Features.RestoreIngredient;

public class RestoreIngredientRequest : IRequest<RestoreIngredientResponse>
{
    public int Id { get; set; }
}