using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Extensions;
using Users.Controllers;
using Users.Core.Extensions;
using Users.Insfrastructure.Extensions;

namespace Users.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUsers(this IServiceCollection services)
    {
        services.AddUsersCore();
        services.AddUsersInfrastructure();
        services.AddSwaggerComments<UsersController>();
        return services;
    }
}
