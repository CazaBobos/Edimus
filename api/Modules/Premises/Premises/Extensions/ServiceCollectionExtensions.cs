using Microsoft.Extensions.DependencyInjection;
using Premises.Controllers;
using Premises.Core.Extensions;
using Premises.Infrastructure.Extensions;
using Shared.Infrastructure.Extensions;

namespace Premises.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPremises(this IServiceCollection services)
    {
        services.AddPremisesCore();
        services.AddPremisesInfrastructure();
        services.AddSwaggerComments<PremisesController>();
        return services;
    }
}
