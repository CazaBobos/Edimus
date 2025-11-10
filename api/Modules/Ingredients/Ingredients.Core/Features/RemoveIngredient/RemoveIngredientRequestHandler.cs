using Ingredients.Core.Abstractions;
using MediatR;
using Shared.Core.Exceptions;

namespace Ingredients.Core.Features.RemoveIngredient;

public class RemoveIngredientRequestHandler : IRequestHandler<RemoveIngredientRequest, RemoveIngredientResponse>
{
    private readonly IIngredientsRepository _ingredientsRepository;

    public RemoveIngredientRequestHandler(IIngredientsRepository ingredientsRepository)
    {
        _ingredientsRepository = ingredientsRepository;
    }

    public async Task<RemoveIngredientResponse> Handle(RemoveIngredientRequest request, CancellationToken cancellationToken)
    {
        var ingredient = await _ingredientsRepository.GetById(request.Id, cancellationToken);

        if (ingredient is null) throw new HttpNotFoundException();

        ingredient.Remove();

        await _ingredientsRepository.Update(ingredient, cancellationToken);

        return new RemoveIngredientResponse();
    }
}