using Ingredients.Core.Abstractions;
using MediatR;
using Shared.Core.Exceptions;

namespace Ingredients.Core.Features.RestoreIngredient;

public class RestoreIngredientRequestHandler : IRequestHandler<RestoreIngredientRequest, RestoreIngredientResponse>
{
    private readonly IIngredientsRepository _ingredientsRepository;

    public RestoreIngredientRequestHandler(IIngredientsRepository ingredientsRepository)
    {
        _ingredientsRepository = ingredientsRepository;
    }

    public async Task<RestoreIngredientResponse> Handle(RestoreIngredientRequest request, CancellationToken cancellationToken)
    {
        var ingredient = await _ingredientsRepository.GetById(request.Id, cancellationToken);

        if (ingredient is null) throw new HttpNotFoundException();

        ingredient.Restore();

        await _ingredientsRepository.Update(ingredient, cancellationToken);

        return new RestoreIngredientResponse();
    }
}