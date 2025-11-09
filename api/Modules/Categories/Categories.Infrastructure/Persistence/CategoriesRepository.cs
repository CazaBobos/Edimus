using Categories.Core.Abstractions;
using Shared.Core.Entities;
using Shared.Infrastructure.Persistence;

namespace Categories.Infrastructure.Persistence;
public class CategoriesRepository : Repository<Category, int>, ICategoriesRepository
{
    public CategoriesRepository(DatabaseContext context) : base(context)
    {
    }
}