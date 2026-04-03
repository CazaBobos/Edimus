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
            Subject = new ClaimsIdentity(GetAccessClaims(user)),
            IssuedAt = DateTime.UtcNow,
            Expires = expires,
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = GetSigningCredentials()
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken(IUserRecord user)
    {
        var expires = DateTime.UtcNow.AddDays(_jwtSettings.RefreshExpirationInDays);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(UserClaims.Id, user.Id.ToString()),
                new Claim("token_type", "refresh"),
            ]),
            IssuedAt = DateTime.UtcNow,
            Expires = expires,
            Issuer = _jwtSettings.Issuer,
            SigningCredentials = GetSigningCredentials()
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public int? ValidateRefreshToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            }, out SecurityToken validatedToken);

            var jwt = (JwtSecurityToken)validatedToken;

            var tokenType = jwt.Claims.FirstOrDefault(c => c.Type == "token_type")?.Value;
            if (tokenType != "refresh") return null;

            var userIdClaim = jwt.Claims.FirstOrDefault(c => c.Type == UserClaims.Id)?.Value;
            return userIdClaim is not null ? int.Parse(userIdClaim) : null;
        }
        catch
        {
            return null;
        }
    }

    private IEnumerable<Claim> GetAccessClaims(IUserRecord user)
    {
        yield return new Claim(UserClaims.Id, user.Id.ToString());
        yield return new Claim(UserClaims.Username, user.Username.ToString());
        yield return new Claim(UserClaims.Role, ((int)user.Role).ToString());
        yield return new Claim(UserClaims.Companies, string.Join(",", user.CompanyIds));
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
