using Tables.Core.Abstractions;
using Tables.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Tables.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTablesInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ITablesRepository, TablesRepository>();
        return services;
    }
}