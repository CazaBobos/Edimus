using Users.Core.Model;

namespace Users.Core.Features.GetUsers;
public class GetUsersResponse
{
    public int Count { get; set; }
    public int? Limit { get; set; }
    public int? Page { get; set; }
    public List<UserModel> Users { get; set; } = new();
}
