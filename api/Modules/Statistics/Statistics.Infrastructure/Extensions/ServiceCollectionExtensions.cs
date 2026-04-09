using Microsoft.Extensions.DependencyInjection;
using Statistics.Core.Abstractions;
using Statistics.Infrastructure.Persistence;

namespace Statistics.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStatisticsInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IStatisticsRepository, StatisticsRepository>();
        return services;
    }
}
