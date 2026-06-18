using Ingredients.Core.Abstractions;
using Mediator;
using Shared.Core.Entities;
using Shared.Core.Exceptions;

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
        if (!request.User.CompanyIds.Contains(request.CompanyId))
            throw new HttpForbiddenException("You don't have access to this company.");

        var ingredient = new Ingredient(request.CompanyId, request.Name, request.Stock, request.Alert, request.Unit);

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