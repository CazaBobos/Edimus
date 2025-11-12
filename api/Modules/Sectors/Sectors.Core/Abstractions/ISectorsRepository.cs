using Shared.Core.Entities;
using Shared.Core.Persistence;

namespace Sectors.Core.Abstractions;

public interface ISectorsRepository : IRepository<Sector, int>
{
}