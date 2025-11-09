using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Edimus.Api.Extensions;
public static class AuthenticationExtensions
{
    public static IServiceCollection AddJWTAuthentication(this IServiceCollection services)
    {
        var jwtSection = services
            .BuildServiceProvider()
            .GetRequiredService<IConfiguration>()
            .GetSection("JWT");

        var key = Encoding.ASCII.GetBytes(jwtSection["Secret"]);

        services
            .AddAuthentication(d =>
            {
                d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(d =>
            {
                d.RequireHttpsMetadata = false;
                d.SaveToken = true;
                d.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true
                    //ClockSkew defaults to 5 minutes, so when setting up expiration,
                    //it just adds to those 5 minutes. For that it's set to TimeSpan.Zero
                };
            });
        return services;
    }
}
