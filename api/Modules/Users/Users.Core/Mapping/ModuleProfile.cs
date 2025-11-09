using AutoMapper;
using Shared.Core.Entities;
using Users.Core.Model;

namespace Users.Core.Mapping;
public class ModuleProfile : Profile
{
    public ModuleProfile()
    {
        CreateMap<User, UserModel>().ReverseMap();
    }
}
