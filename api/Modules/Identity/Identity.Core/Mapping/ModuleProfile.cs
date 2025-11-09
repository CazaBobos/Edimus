using AutoMapper;
using Shared.Core.Entities;
using Identity.Core.Model;

namespace Identity.Core.Mapping;
public class ModuleProfile : Profile
{
    public ModuleProfile()
    {
        CreateMap<User, UserModel>().ReverseMap();
    }
}
