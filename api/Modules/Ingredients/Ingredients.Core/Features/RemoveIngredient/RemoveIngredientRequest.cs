using Mediator;

namespace Ingredients.Core.Features.RemoveIngredient;

public class RemoveIngredientRequest : IRequest<RemoveIngredientResponse>
{
    public int Id { get; set; }
}