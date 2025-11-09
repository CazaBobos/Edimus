using Companies.Controllers;
using Companies.Core.Extensions;
using Companies.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Extensions;

namespace Companies.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCompanies(this IServiceCollection services)
    {
        services.AddCompaniesCore();
        services.AddCompaniesInfrastructure();
        services.AddSwaggerComments<CompaniesController>();
        return services;
    }
}