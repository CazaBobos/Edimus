using AutoMapper;
using Ingredients.Core.Model;
using Shared.Core.Entities;

namespace Ingredients.Core.Mapping;

public class IngredientProfile : Profile
{
    public IngredientProfile()
    {
        CreateMap<Ingredient, IngredientModel>()
            .ReverseMap();
    }
}