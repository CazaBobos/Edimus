using Categories.Core.Abstractions;
using Categories.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Categories.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCategoriesInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        return services;
    }
}