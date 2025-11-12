using Sectors.Core.Abstractions;
using Shared.Core.Entities;
using Shared.Infrastructure.Persistence;

namespace Sectors.Infrastructure.Persistence;
public class SectorsRepository : Repository<Sector, int>, ISectorsRepository
{
    public SectorsRepository(DatabaseContext context) : base(context)
    {
    }
}