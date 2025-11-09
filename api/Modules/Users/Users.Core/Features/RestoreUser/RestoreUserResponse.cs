using Users.Core.Model;

namespace Users.Core.Features.RestoreUser;
public class RestoreUserResponse
{
    public UserModel User { get; set; } = new();
}
