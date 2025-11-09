using Shared.Core.Entities;

namespace Users.Input;

public class UpdateUserPasswordInput
{
    public string CurrentPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}

public class UpdateUserEmailInput
{
    public string Email { get; set; } = string.Empty;
}

public class UpdateUserNameInput
{
    public string Username { get; set; } = string.Empty;
}
public class UpdateUserRoleInput
{
    public UserRole RoleId { get; set; }
}