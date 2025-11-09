using Categories.Controllers;
using Categories.Core.Extensions;
using Categories.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Extensions;

namespace Categories.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCategories(this IServiceCollection services)
    {
        services.AddCategoriesCore();
        services.AddCategoriesInfrastructure();
        services.AddSwaggerComments<CategoriesController>();
        return services;
    }
}