using Shared.Core.Entities;
using Shared.Core.Persistence;

namespace Ingredients.Core.Abstractions;

public interface IIngredientsRepository : IRepository<Ingredient, int>
{
}