using Microsoft.Extensions.DependencyInjection;
using Layouts.Core.Abstractions;
using Shared.Core.Extensions;

namespace Layouts.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLayoutsCore(this IServiceCollection services)
    {
        services.AddMapster<ILayoutsRepository>();
        return services;
    }
}
