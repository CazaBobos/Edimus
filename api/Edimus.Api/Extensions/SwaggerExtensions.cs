using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace Edimus.Api.Extensions;
public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerWithAuth(this IServiceCollection services)
    {
        services.AddSwaggerGen(setup =>
        {
            setup.CustomSchemaIds(x => x.FullName);

            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            //uses jwtSecurityScheme.* except .Reference
            setup.AddSecurityDefinition(jwtSecurityScheme.Scheme, jwtSecurityScheme);
            //uses jwtSecurityScheme.Reference
            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { jwtSecurityScheme, Array.Empty<string>()  }
            });
        });

        return services;
    }
}
