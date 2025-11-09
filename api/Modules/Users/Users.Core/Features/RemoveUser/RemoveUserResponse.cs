using Users.Core.Model;

namespace Users.Core.Features.RemoveUser;
public class RemoveUserResponse
{
    public UserModel User { get; set; } = new();
}
