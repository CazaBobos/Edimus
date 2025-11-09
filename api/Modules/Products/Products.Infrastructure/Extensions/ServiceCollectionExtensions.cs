using Microsoft.Extensions.DependencyInjection;
using Products.Core.Abstractions;
using Products.Infrastructure.Persistence;

namespace Products.Infrastructure.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProductsInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IProductsRepository, ProductsRepository>();
        return services;
    }
}