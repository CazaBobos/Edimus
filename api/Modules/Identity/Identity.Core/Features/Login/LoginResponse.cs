using Shared.Core.Entities;
using System.Text.Json.Serialization;

namespace Identity.Core.Features.Login;
public class LoginResponse
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<int> CompanyIds { get; set; } = [];
    public UserRole Role { get; set; }
    public long TokenExpiresAt { get; set; }
    public long RefreshTokenExpiresAt { get; set; }

    [JsonIgnore]
    public string Token { get; set; } = string.Empty;
    [JsonIgnore]
    public string RefreshToken { get; set; } = string.Empty;
}
