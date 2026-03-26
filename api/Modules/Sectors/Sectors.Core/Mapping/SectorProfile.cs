using Mapster;
using Sectors.Core.Model;
using Shared.Core.Entities;

namespace Sectors.Core.Mapping;

public class SectorProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Sector, SectorModel>().TwoWays();
        config.NewConfig<SectorCoord, SectorCoordModel>().TwoWays();
    }
}