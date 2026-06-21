using AuditLogs.Controllers;
using AuditLogs.Core.Extensions;
using AuditLogs.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Extensions;

namespace AuditLogs.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuditLogs(this IServiceCollection services)
    {
        services.AddAuditLogsCore();
        services.AddAuditLogsInfrastructure();
        services.AddSwaggerComments<AuditLogsController>();
        return services;
    }
}
