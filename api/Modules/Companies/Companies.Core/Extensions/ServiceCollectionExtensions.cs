using Companies.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;
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