using Categories.Core.Abstractions;
using Mediator;
using Shared.Core.Entities;
using Shared.Infrastructure.Persistence;

namespace Categories.Infrastructure.Persistence;
public class CategoriesRepository : Repository<Category, int>, ICategoriesRepository
{
    public CategoriesRepository(DatabaseContext context, IPublisher publisher) : base(context, publisher)
    {
    }
}
