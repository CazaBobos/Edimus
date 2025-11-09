using Shared.Core.Entities;
using Shared.Core.Persistence;

namespace Users.Core.Abstractions;
public interface IUsersRepository : IRepository<User, int>
{
}