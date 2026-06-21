using AuditLogs.Core.Abstractions;
using AuditLogs.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace AuditLogs.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuditLogsInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAuditLogsRepository, AuditLogsRepository>();
        return services;
    }
}
