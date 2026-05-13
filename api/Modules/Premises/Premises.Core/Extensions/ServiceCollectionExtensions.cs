using Microsoft.Extensions.DependencyInjection;
using Premises.Core.Abstractions;
using Shared.Core.Extensions;

namespace Premises.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPremisesCore(this IServiceCollection services)
    {
        services.AddMapster<IPremisesRepository>();
        return services;
    }
}
