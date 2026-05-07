using Mapster;
using Shared.Core.Entities;
using Tags.Core.Model;

namespace Tags.Core.Mapping;

public class TagProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Tag, TagModel>().TwoWays();
    }
}
