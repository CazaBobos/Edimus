using Ingredients.Core.Abstractions;
using MediatR;
using Shared.Core.Exceptions;

namespace Ingredients.Core.Features.UpdateIngredient;

public class UpdateIngredientRequestHandler : IRequestHandler<UpdateIngredientRequest, UpdateIngredientResponse>
{
    private readonly IIngredientsRepository _ingredientsRepository;

    public UpdateIngredientRequestHandler(IIngredientsRepository ingredientsRepository)
    {
        _ingredientsRepository = ingredientsRepository;
    }

    public async Task<UpdateIngredientResponse> Handle(UpdateIngredientRequest request, CancellationToken cancellationToken)
    {
        var ingredient = await _ingredientsRepository.GetById(request.Id, cancellationToken);

        if (ingredient is null) throw new HttpNotFoundException();

        ingredient.Update(request.Name, request.Stock, request.Alert, request.Unit);

        await _ingredientsRepository.Update(ingredient, cancellationToken);

        return new UpdateIngredientResponse();
    }
}