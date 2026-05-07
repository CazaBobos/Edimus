using Shared.Core.Entities;
using Shared.Core.Persistence;

namespace Tags.Core.Abstractions;

public interface ITagsRepository : IRepository<Tag, int>
{
}
