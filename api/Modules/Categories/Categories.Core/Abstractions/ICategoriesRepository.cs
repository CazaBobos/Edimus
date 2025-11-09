using Shared.Core.Entities;
using Shared.Core.Persistence;

namespace Categories.Core.Abstractions;

public interface ICategoriesRepository : IRepository<Category, int>
{
}