using Microsoft.Extensions.DependencyInjection;
using Premises.Infrastructure.Persistence;
using Premises.Core.Abstractions;

namespace Premises.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPremisesInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IPremisesRepository, PremisesRepository>();
        return services;
    }
}
