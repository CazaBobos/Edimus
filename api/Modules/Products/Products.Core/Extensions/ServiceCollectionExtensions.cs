using Microsoft.Extensions.DependencyInjection;
using Products.Core.Abstractions;
using Shared.Core.Extensions;

namespace Products.Core.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProductsCore(this IServiceCollection services)
    {
        services.AddMediatR<IProductsRepository>();
        services.AddAutoMapper<IProductsRepository>();
        return services;
    }
}