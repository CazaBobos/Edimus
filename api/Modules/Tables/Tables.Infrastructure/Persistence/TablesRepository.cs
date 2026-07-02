using Mediator;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Entities;
using Shared.Infrastructure.Persistence;
using Tables.Core.Abstractions;

namespace Tables.Infrastructure.Persistence;

public class TablesRepository : Repository<Table, int>, ITablesRepository
{
    public TablesRepository(DatabaseContext context, IPublisher publisher) : base(context, publisher)
    {
    }

    public async Task<bool> GetCompanyReactiveStock(int tableId, CancellationToken cancellationToken)
    {
        return await _context.Set<Table>()
            .Where(t => t.Id == tableId)
            .Select(t => t.Layout!.Premise!.Company!.ReactiveStock)
            .FirstOrDefaultAsync(cancellationToken);
    }
}