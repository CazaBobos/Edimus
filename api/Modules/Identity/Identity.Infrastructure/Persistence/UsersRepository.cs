using Identity.Core.Abstractions;
using Mediator;
using Shared.Core.Entities;
using Shared.Infrastructure.Persistence;

namespace Identity.Insfrastructure.Persistence;
public class UsersRepository : Repository<User, int>, IUsersRepository
{
    public UsersRepository(DatabaseContext context, IPublisher publisher) : base(context, publisher)
    {
    }
}