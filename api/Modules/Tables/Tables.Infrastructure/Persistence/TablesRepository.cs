using Shared.Core.Entities;
using Shared.Infrastructure.Persistence;
using Tables.Core.Abstractions;

namespace Tables.Infrastructure.Persistence;
public class TablesRepository : Repository<Table, int>, ITablesRepository
{
    public TablesRepository(DatabaseContext context) : base(context)
    {
    }
}