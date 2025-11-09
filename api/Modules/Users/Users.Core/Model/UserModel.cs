using Shared.Core.Entities;

namespace Users.Core.Model;

public class UserModel
{
    public long Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public List<long> CompanyIds { get; set; } = new();
}
