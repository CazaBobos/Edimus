using Microsoft.Extensions.DependencyInjection;
using Sectors.Core.Abstractions;
using Shared.Core.Extensions;

namespace Sectors.Core.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSectorsCore(this IServiceCollection services)
    {
        services.AddMediatR<ISectorsRepository>();
        services.AddAutoMapper<ISectorsRepository>();

        return services;
    }
}