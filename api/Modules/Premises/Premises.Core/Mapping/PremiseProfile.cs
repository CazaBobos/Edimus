using Mapster;
using Premises.Core.Model;
using Shared.Core.Entities;

namespace Premises.Core.Mapping;

public class PremiseProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Premise, PremiseModel>();
    }
}
