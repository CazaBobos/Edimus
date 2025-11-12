using Microsoft.IdentityModel.Tokens;
using Shared.Core.Abstractions;
using Shared.Core.Services;
using Shared.Core.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shared.Infrastructure.Security;
public class JwtService : IJwtService
{
    public IJwtSettings _jwtSettings { get; set; }

    public JwtService(IJwtSettings jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }

    public string GenerateToken(IUserRecord user)
    {
        var expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(GetClaims(user)),
            IssuedAt = DateTime.UtcNow,
            Expires = expires,
            SigningCredentials = GetSigningCredentials()
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private IEnumerable<Claim> GetClaims(IUserRecord user)
    {
        yield return new Claim(UserClaims.Id, user.Id.ToString());
        yield return new Claim(UserClaims.Username, user.Username.ToString());
        yield return new Claim(UserClaims.Role, ((int)user.Role).ToString());
        yield return new Claim(UserClaims.Companies, user.CompanyIds?.ToString() ?? "[]");
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
        return new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256
            );
    }
}
