using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Extensions;
using Identity.Controllers;
using Identity.Core.Extensions;
using Identity.Insfrastructure.Extensions;

namespace Identity.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentityCore();
        services.AddIdentityInfrastructure();
        services.AddSwaggerComments<IdentityController>();
        return services;
    }
}
