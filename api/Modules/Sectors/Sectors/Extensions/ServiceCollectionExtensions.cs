using Microsoft.Extensions.DependencyInjection;
using Sectors.Controllers;
using Sectors.Core.Extensions;
using Sectors.Infrastructure.Extensions;
using Shared.Infrastructure.Extensions;

namespace Sectors.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSectors(this IServiceCollection services)
    {
        services.AddSectorsCore();
        services.AddSectorsInfrastructure();
        services.AddSwaggerComments<SectorsController>();
        return services;
    }
}