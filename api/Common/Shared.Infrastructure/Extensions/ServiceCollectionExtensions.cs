using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Services;
using Shared.Core.Settings;
using Shared.Infrastructure.Persistence;
using Shared.Infrastructure.Security;
using Shared.Infrastructure.Services;

namespace Shared.Infrastructure.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabaseContext(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddDbContext<DatabaseContext>();

        return services;
    }

    public static IServiceCollection AddJwtService(this IServiceCollection services)
    {
        services.AddScoped<IJwtSettings, JwtSettings>();
        services.AddScoped<IJwtService, JwtService>();

        return services;
    }

    public static IServiceCollection AddMailService(this IServiceCollection services)
    {
        services.AddScoped<MailSettings>();
        services.AddScoped<IMailService, MailService>();

        return services;
    }

}
