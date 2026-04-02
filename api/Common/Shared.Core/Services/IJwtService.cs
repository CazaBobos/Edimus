using Shared.Core.Abstractions;

namespace Shared.Core.Services;
public interface IJwtService
{
    public string GenerateToken(IUserRecord user);
    public string GenerateRefreshToken(IUserRecord user);
    public int? ValidateRefreshToken(string token);
}
