using Mapster;
using Identity.Core.Model;
using Shared.Core.Entities;

namespace Identity.Core.Mapping;
public class ModuleProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserModel>().TwoWays();
    }
}
