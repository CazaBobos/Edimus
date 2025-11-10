using MediatR;
using Shared.Core.Abstractions;

namespace Ingredients.Core.Features.RemoveIngredient;

public class RemoveIngredientRequest : IRequest<RemoveIngredientResponse>
{
    public int Id { get; set; }
    public IUserRecord User { get; set; } = null!;
}