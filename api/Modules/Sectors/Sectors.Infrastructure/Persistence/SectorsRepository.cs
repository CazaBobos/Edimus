using Sectors.Core.Abstractions;
using Shared.Infrastructure.Persistence;
using Shared.Core.Entities;

namespace Sectors.Infrastructure.Persistence;
public class SectorsRepository : Repository<Sector, int>, ISectorsRepository
{
    public SectorsRepository(DatabaseContext context) : base(context)
    {
    }
}