using Microsoft.Extensions.Configuration;
using Shared.Core.Settings;

namespace Shared.Infrastructure.Security;
public class JwtSettings : IJwtSettings
{
    public required string Audience { get; set; }
    public required string Issuer { get; set; }
    public required string Secret { get; set; }
    public required int ExpirationInMinutes { get; set; }
    public required int RefreshExpirationInDays { get; set; }

    public JwtSettings(IConfiguration config)
    {

        Audience = config.GetSection("JWT:Audience").Value
            ?? throw new InvalidOperationException("Audience not found in app settings file");

        Issuer = config.GetSection("JWT:Issuer").Value
            ?? throw new InvalidOperationException("Issuer not found in app settings file");

        var secret = config.GetSection("JWT:Secret").Value
            ?? throw new InvalidOperationException("Secret not found in app settings file");
        Secret = secret;

        var expiration = config.GetSection("JWT:Expiration").Value
            ?? throw new InvalidOperationException("Expiration not found in app settings file");
        ExpirationInMinutes = int.Parse(expiration);

        var refreshExpiration = config.GetSection("JWT:RefreshExpiration").Value
            ?? throw new InvalidOperationException("RefreshExpiration not found in app settings file");
        RefreshExpirationInDays = int.Parse(refreshExpiration);
    }
}
