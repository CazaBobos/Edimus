using Ingredients.Core.Abstractions;
using Mediator;
using Shared.Core.Entities;

namespace Ingredients.Core.Features.CreateIngredient;

public class CreateIngredientRequestHandler : IRequestHandler<CreateIngredientRequest, CreateIngredientResponse>
{
    private readonly IIngredientsRepository _ingredientsRepository;

    public CreateIngredientRequestHandler(IIngredientsRepository ingredientsRepository)
    {
        _ingredientsRepository = ingredientsRepository;
    }

    public async ValueTask<CreateIngredientResponse> Handle(CreateIngredientRequest request, CancellationToken cancellationToken)
    {
        var ingredient = new Ingredient(request.Name, request.Stock, request.Alert, request.Unit);

        var existingIngredient = await _ingredientsRepository
            .FindOne(x => x.Id == ingredient.Id || x.Name == ingredient.Name);

        if (existingIngredient is not null)
            throw new InvalidOperationException("The ingredient already exists");

        await _ingredientsRepository.Add(ingredient, cancellationToken);

        return new CreateIngredientResponse
        {
            Id = ingredient.Id
        };
    }
}