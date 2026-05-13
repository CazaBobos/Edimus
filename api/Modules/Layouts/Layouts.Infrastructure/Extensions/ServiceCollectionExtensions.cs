using Layouts.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Layouts.Core.Abstractions;

namespace Layouts.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLayoutsInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ILayoutsRepository, LayoutsRepository>();
        return services;
    }
}
