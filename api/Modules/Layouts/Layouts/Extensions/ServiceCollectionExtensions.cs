using Layouts.Controllers;
using Layouts.Core.Extensions;
using Layouts.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Extensions;

namespace Layouts.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLayouts(this IServiceCollection services)
    {
        services.AddLayoutsCore();
        services.AddLayoutsInfrastructure();
        services.AddSwaggerComments<LayoutsController>();
        return services;
    }
}
