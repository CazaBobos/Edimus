using Mapster;
using Ingredients.Core.Model;
using Shared.Core.Entities;

namespace Ingredients.Core.Mapping;

public class IngredientProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Ingredient, IngredientModel>().TwoWays();
    }
}