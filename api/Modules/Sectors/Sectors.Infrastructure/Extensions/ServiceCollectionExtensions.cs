using Sectors.Core.Abstractions;
using Sectors.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Sectors.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSectorsInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ISectorsRepository, SectorsRepository>();
        return services;
    }
}