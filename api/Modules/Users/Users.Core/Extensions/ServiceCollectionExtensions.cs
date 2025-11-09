using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Extensions;
using Users.Core.Abstractions;

namespace Users.Core.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUsersCore(this IServiceCollection services)
    {
        services.AddMediatR<IUsersRepository>();
        services.AddAutoMapper<IUsersRepository>();
        return services;
    }
}
