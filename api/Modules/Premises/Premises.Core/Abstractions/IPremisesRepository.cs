using Shared.Core.Entities;
using Shared.Core.Persistence;

namespace Premises.Core.Abstractions;

public interface IPremisesRepository : IRepository<Premise, int>
{
}
