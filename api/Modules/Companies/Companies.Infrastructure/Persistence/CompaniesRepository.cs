using Companies.Core.Abstractions;
using Mediator;
using Shared.Core.Entities;
using Shared.Infrastructure.Persistence;

namespace Companies.Infrastructure.Persistence;

public class CompaniesRepository : Repository<Company, int>, ICompaniesRepository
{
    public CompaniesRepository(DatabaseContext context, IPublisher publisher) : base(context, publisher)
    {
    }
}