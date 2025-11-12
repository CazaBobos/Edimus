using Identity.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Extensions;

namespace Identity.Core.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityCore(this IServiceCollection services)
    {
        services.AddMediatR<IUsersRepository>();
        services.AddAutoMapper<IUsersRepository>();
        return services;
    }
}
