using AutoMapper;
using Identity.Core.Model;
using Shared.Core.Entities;

namespace Identity.Core.Mapping;
public class ModuleProfile : Profile
{
    public ModuleProfile()
    {
        CreateMap<User, UserModel>().ReverseMap();
    }
}
