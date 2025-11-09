using Companies.Core.Abstractions;
using Companies.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Companies.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCompaniesInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ICompaniesRepository, CompaniesRepository>();
        return services;
    }
}