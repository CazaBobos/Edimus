using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Infrastructure.Extensions;
public static class SwaggerCommentsExtensions
{
    public static IServiceCollection AddSwaggerComments<T>(this IServiceCollection services) where T : ControllerBase
    {
        services.AddSwaggerGen(setup =>
        {
            var xmlFilename = $"{typeof(T).Assembly.GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
            setup.IncludeXmlComments(xmlPath);
        });

        return services;
    }
}
