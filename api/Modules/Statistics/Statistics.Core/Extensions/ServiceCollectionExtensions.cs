using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Extensions;
using Statistics.Core.Abstractions;

namespace Statistics.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStatisticsCore(this IServiceCollection services)
    {
        services.AddMapster<IStatisticsRepository>();
        return services;
    }
}
