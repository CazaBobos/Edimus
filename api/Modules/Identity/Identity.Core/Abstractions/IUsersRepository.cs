using Shared.Core.Entities;
using Shared.Core.Persistence;

namespace Identity.Core.Abstractions;
public interface IUsersRepository : IRepository<User, int>
{
}