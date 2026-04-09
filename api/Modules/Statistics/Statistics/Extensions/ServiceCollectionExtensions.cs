using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Extensions;
using Statistics.Controllers;
using Statistics.Core.Extensions;
using Statistics.Infrastructure.Extensions;

namespace Statistics.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStatistics(this IServiceCollection services)
    {
        services.AddStatisticsCore();
        services.AddStatisticsInfrastructure();
        services.AddSwaggerComments<StatisticsController>();
        return services;
    }
}
