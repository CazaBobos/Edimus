using Ingredients.Core.Abstractions;
using Shared.Core.Entities;
using Shared.Infrastructure.Persistence;

namespace Ingredients.Infrastructure.Persistence;
public class IngredientsRepository : Repository<Ingredient, int>, IIngredientsRepository
{
    public IngredientsRepository(DatabaseContext context) : base(context)
    {
    }
}