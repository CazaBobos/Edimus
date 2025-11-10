using Ingredients.Controllers;
using Ingredients.Core.Extensions;
using Ingredients.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Extensions;

namespace Ingredients.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIngredients(this IServiceCollection services)
    {
        services.AddIngredientsCore();
        services.AddIngredientsInfrastructure();
        services.AddSwaggerComments<IngredientsController>();
        return services;
    }
}