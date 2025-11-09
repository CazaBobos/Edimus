using Microsoft.Extensions.DependencyInjection;
using Categories.Core.Abstractions;
using Shared.Core.Extensions;

namespace Categories.Core.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCategoriesCore(this IServiceCollection services)
    {
        services.AddMediatR<ICategoriesRepository>();
        services.AddAutoMapper<ICategoriesRepository>();
        return services;
    }
}