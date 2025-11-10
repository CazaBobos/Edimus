using Ingredients.Core.Abstractions;
using Ingredients.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Ingredients.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIngredientsInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IIngredientsRepository, IngredientsRepository>();
        return services;
    }
}