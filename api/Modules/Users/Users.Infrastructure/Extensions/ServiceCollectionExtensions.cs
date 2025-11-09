using Microsoft.Extensions.DependencyInjection;
using Users.Core.Abstractions;
using Users.Insfrastructure.Persistence;

namespace Users.Insfrastructure.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUsersInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUsersRepository, UsersRepository>();
        return services;
    }
}
