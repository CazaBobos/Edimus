using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMapster<T>(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(typeof(T).Assembly);
        return services;
    }
}
