using AuditLogs.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Extensions;

namespace AuditLogs.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuditLogsCore(this IServiceCollection services)
    {
        services.AddMapster<IAuditLogsRepository>();
        return services;
    }
}
