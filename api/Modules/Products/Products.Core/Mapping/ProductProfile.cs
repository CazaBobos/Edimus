using Mapster;
using Products.Core.Model;
using Shared.Core.Entities;

namespace Products.Core.Mapping;

public class ProductProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Product, ProductModel>().TwoWays();
        config.NewConfig<Consumption, ConsumptionModel>();
    }
}