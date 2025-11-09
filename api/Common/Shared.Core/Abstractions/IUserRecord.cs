using Shared.Core.Entities;

namespace Shared.Core.Abstractions;

public interface IUserRecord
{
    int Id { get; }
    string Username { get; }
    UserRole Role { get; }
    List<int> CompanyIds { get; }
}
