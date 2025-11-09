using Shared.Core.Entities;
using Shared.Core.Persistence;

namespace Tables.Core.Abstractions;

public interface ITablesRepository : IRepository<Table, int>
{
}