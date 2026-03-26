using Mapster;
using Shared.Core.Entities;
using Tables.Core.Model;

namespace Tables.Core.Mapping;

public class TableProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Table, TableModel>().TwoWays();
        config.NewConfig<TableCoord, TableCoordModel>().TwoWays();
        config.NewConfig<Order, OrderModel>().TwoWays();
    }
}