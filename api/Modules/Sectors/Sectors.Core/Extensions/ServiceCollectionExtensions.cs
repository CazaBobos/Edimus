using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Extensions;
using Sectors.Core.Abstractions;

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