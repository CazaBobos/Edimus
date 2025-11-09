using Users.Core.Model;

namespace Users.Core.Features.CreateUser;
public class CreateUserResponse
{
    public UserModel User { get; set; } = new();
}
