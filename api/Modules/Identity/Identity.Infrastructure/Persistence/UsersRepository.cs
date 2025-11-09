using Shared.Core.Entities;
using Shared.Infrastructure.Persistence;
using Identity.Core.Abstractions;

namespace Identity.Insfrastructure.Persistence;
public class UsersRepository : Repository<User, int>, IUsersRepository
{
    public UsersRepository(DatabaseContext context) : base(context)
    {
    }
}