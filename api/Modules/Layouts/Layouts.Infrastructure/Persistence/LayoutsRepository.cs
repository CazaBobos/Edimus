using Mediator;
using Shared.Core.Entities;
using Layouts.Core.Abstractions;
using Shared.Infrastructure.Persistence;

namespace Layouts.Infrastructure.Persistence;

public class LayoutsRepository : Repository<Layout, int>, ILayoutsRepository
{
    public LayoutsRepository(DatabaseContext context, IPublisher publisher) : base(context, publisher)
    {
    }
}
