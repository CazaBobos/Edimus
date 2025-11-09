using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Extensions;
using Tables.Core.Abstractions;

namespace Tables.Core.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTablesCore(this IServiceCollection services)
    {
        services.AddMediatR<ITablesRepository>();
        services.AddAutoMapper<ITablesRepository>();
        
        return services;
    }
}