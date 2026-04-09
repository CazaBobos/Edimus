using Microsoft.Extensions.DependencyInjection;
using Tables.Core.Abstractions;
using Tables.Infrastructure.Persistence;

namespace Tables.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTablesInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ITablesRepository, TablesRepository>();
        services.AddScoped<ITableSessionRepository, TableSessionRepository>();
        return services;
    }
}