using Ingredients.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Extensions;

namespace Ingredients.Core.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIngredientsCore(this IServiceCollection services)
    {
        services.AddMapster<IIngredientsRepository>();
        return services;
    }
}