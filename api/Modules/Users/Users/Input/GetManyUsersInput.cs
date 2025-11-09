using Shared.Core.Entities;

namespace Users.Input;
public class GetManyUsersInput
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public UserRole? RoleId { get; set; }
}
