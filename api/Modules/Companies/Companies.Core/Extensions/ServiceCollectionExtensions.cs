using Microsoft.Extensions.DependencyInjection;
using Companies.Core.Abstractions;
using Shared.Core.Extensions;

namespace Companies.Core.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCompaniesCore(this IServiceCollection services)
    {
        services.AddMediatR<ICompaniesRepository>();
        services.AddAutoMapper<ICompaniesRepository>();
        return services;
    }
}