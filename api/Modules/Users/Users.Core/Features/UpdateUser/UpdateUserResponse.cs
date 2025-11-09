using Users.Core.Model;

namespace Users.Core.Features.UpdateUser;
public class UpdateUserResponse
{
    public UserModel User { get; set; } = new();
}
