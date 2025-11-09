using Shared.Core.Entities;
using Shared.Infrastructure.Persistence;
using Users.Core.Abstractions;

namespace Users.Insfrastructure.Persistence;
public class UsersRepository : Repository<User, int>, IUsersRepository
{
    public UsersRepository(DatabaseContext context) : base(context)
    {
    }
}