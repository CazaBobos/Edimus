using Shared.Core.Entities;

namespace Identity.Core.Features.Login;
public class LoginResponse
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<int> CompanyIds { get; set; } = new();
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public int ExpiresIn { get; set; }
}
