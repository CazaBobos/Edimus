using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Edimus.Api.Extensions;
public static class AuthenticationExtensions
{
    public static IServiceCollection AddJWTAuthentication(this IServiceCollection services, IWebHostEnvironment environment)
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
                d.RequireHttpsMetadata = !environment.IsDevelopment();
                d.SaveToken = true;
                d.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["token"];
                        return Task.CompletedTask;
                    }
                };
                d.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSection["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = jwtSection["Audience"],
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true
                    //ClockSkew defaults to 5 minutes, so when setting up expiration,
                    //it just adds to those 5 minutes. For that it's set to TimeSpan.Zero
                };
            });

        return services;
    }
}
