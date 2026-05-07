using Mediator;
using Shared.Core.Entities;
using Shared.Infrastructure.Persistence;
using Tags.Core.Abstractions;

namespace Tags.Infrastructure.Persistence;

public class TagsRepository : Repository<Tag, int>, ITagsRepository
{
    public TagsRepository(DatabaseContext context, IPublisher publisher) : base(context, publisher)
    {
    }
}
