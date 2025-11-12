using Identity.Core.Abstractions;
using Identity.Insfrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Insfrastructure.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUsersRepository, UsersRepository>();
        return services;
    }
}
