using Mapster;
using Categories.Core.Model;
using Shared.Core.Entities;

namespace Categories.Core.Mapping;

public class CategoryProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Category, CategoryModel>().TwoWays();
    }
}