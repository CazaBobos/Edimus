using Microsoft.Extensions.Configuration;
using Shared.Core.Settings;

namespace Shared.Infrastructure.Security;
public class JwtSettings: IJwtSettings
{
    public required string Secret { get; set; } 
    public required int ExpirationInMinutes { get; set; }

    public JwtSettings(IConfiguration config)
    {
        var secret = config.GetSection("JWT:Secret").Value;
        if (secret is null) throw new InvalidOperationException("Secret not found in app settings file");
        Secret = secret;

        var expiration = config.GetSection("JWT:Expiration").Value;
        if (expiration is null) throw new InvalidOperationException("Expiration not found in app settings file");
        ExpirationInMinutes = int.Parse(expiration);
    }
}
