using Layouts.Core.Model;
using Mapster;
using Shared.Core.Entities;

namespace Layouts.Core.Mapping;

public class LayoutProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Layout, LayoutModel>();
        config.NewConfig<LayoutCoord, LayoutCoordModel>();
    }
}
