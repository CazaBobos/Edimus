using Ingredients.Core.Model;

namespace Ingredients.Core.Features.GetManyIngredients;

public class GetIngredientsResponse
{
    public int Count { get; set; }
    public int? Limit { get; set; }
    public int? Page { get; set; }
    public List<IngredientModel> Ingredients { get; set; } = new();
}