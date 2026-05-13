using Mediator;
using Shared.Core.Entities;
using Premises.Core.Abstractions;
using Shared.Infrastructure.Persistence;

namespace Premises.Infrastructure.Persistence;

public class PremisesRepository : Repository<Premise, int>, IPremisesRepository
{
    public PremisesRepository(DatabaseContext context, IPublisher publisher) : base(context, publisher)
    {
    }
}
