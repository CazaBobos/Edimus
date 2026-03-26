using Mapster;
using Companies.Core.Model;
using Shared.Core.Entities;

namespace Companies.Core.Mapping;

public class CompanyProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Company, CompanyModel>().TwoWays();
        config.NewConfig<Premise, PremiseModel>().TwoWays();
        config.NewConfig<Layout, LayoutModel>().TwoWays();
        config.NewConfig<LayoutCoord, LayoutCoordModel>().TwoWays();
    }
}