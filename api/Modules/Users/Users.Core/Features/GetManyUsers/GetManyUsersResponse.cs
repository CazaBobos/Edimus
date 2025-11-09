using Users.Core.Model;

namespace Users.Core.Features.GetManyUsers;
public class GetManyUsersResponse
{
    public List<UserModel> Users { get; set; } = new();
}
