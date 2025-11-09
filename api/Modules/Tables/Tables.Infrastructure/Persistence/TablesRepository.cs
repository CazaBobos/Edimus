using Tables.Core.Abstractions;
using Shared.Infrastructure.Persistence;
using Shared.Core.Entities;

namespace Tables.Infrastructure.Persistence;
public class TablesRepository : Repository<Table, int>, ITablesRepository
{
    public TablesRepository(DatabaseContext context) : base(context)
    {
    }
}