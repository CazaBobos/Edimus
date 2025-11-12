using Identity.Core.Abstractions;
using Shared.Core.Entities;
using Shared.Infrastructure.Persistence;

namespace Identity.Insfrastructure.Persistence;
public class UsersRepository : Repository<User, int>, IUsersRepository
{
    public UsersRepository(DatabaseContext context) : base(context)
    {
    }
}