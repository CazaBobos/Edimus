using Shared.Core.Entities;
using Shared.Core.Persistence;

namespace Companies.Core.Abstractions;

public interface ICompaniesRepository : IRepository<Company, int>
{
}