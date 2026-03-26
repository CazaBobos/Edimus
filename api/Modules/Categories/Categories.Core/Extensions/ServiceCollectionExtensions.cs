using Categories.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Extensions;

namespace Categories.Core.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCategoriesCore(this IServiceCollection services)
    {
        services.AddMapster<ICategoriesRepository>();
        return services;
    }
}