using Mapster;
using Shared.Core.Entities;
using Users.Core.Model;

namespace Users.Core.Mapping;
public class ModuleProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserModel>().TwoWays();
    }
}
