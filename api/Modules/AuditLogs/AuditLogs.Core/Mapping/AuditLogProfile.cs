using AuditLogs.Core.Model;
using Mapster;
using Shared.Core.Entities;

namespace AuditLogs.Core.Mapping;

public class AuditLogProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuditLog, AuditLogModel>().TwoWays();
        config.NewConfig<AuditLogChange, AuditLogChangeModel>().TwoWays();
    }
}
