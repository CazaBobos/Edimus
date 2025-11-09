using Microsoft.Extensions.DependencyInjection;
using Products.Controllers;
using Products.Core.Extensions;
using Products.Infrastructure.Extensions;
using Shared.Infrastructure.Extensions;

namespace Products.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProducts(this IServiceCollection services)
    {
        services.AddProductsCore();
        services.AddProductsInfrastructure();
        services.AddSwaggerComments<ProductsController>();
        return services;
    }
}