using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Extensions;
using Tables.Controllers;
using Tables.Core.Extensions;
using Tables.Infrastructure.Extensions;

namespace Tables.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTables(this IServiceCollection services)
    {
        services.AddTablesCore();
        services.AddTablesInfrastructure();
        services.AddSwaggerComments<TablesController>();
        return services;
    }
}