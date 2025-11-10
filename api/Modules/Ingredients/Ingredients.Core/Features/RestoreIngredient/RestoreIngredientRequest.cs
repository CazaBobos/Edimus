using MediatR;
using Shared.Core.Abstractions;

namespace Ingredients.Core.Features.RestoreIngredient;

public class RestoreIngredientRequest : IRequest<RestoreIngredientResponse>
{
    public int Id { get; set; }
    public IUserRecord User { get; set; } = null!;
}