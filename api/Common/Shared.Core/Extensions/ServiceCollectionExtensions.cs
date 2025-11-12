using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Core.Extensions;

public static class ServiceCollectionExtensions
{
    private static string GetLicenseKey(this IServiceCollection services, string licenseKey)
    {
        var licenseKeysSection = services
            .BuildServiceProvider()
            .GetRequiredService<IConfiguration>()
            .GetSection("LicenseKeys");

        return licenseKeysSection[licenseKey] ?? "";
    }

    public static IServiceCollection AddMediatR<T>(this IServiceCollection services)
    {
        string licenseKey = services.GetLicenseKey("MediatR");

        services.AddMediatR(config =>
        {
            config.LicenseKey = licenseKey;
            config.RegisterServicesFromAssembly(typeof(T).Assembly);
        });

        return services;
    }

    public static IServiceCollection AddAutoMapper<T>(this IServiceCollection services)
    {
        string licenseKey = services.GetLicenseKey("MediatR");

        services.AddAutoMapper(config => config.LicenseKey = licenseKey, typeof(T).Assembly);

        return services;
    }

}
