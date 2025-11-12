using AutoMapper;
using Companies.Core.Model;
using Shared.Core.Entities;

namespace Companies.Core.Mapping;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<Company, CompanyModel>().ReverseMap();
        CreateMap<Premise, PremiseModel>().ReverseMap();
        CreateMap<Layout, LayoutModel>().ReverseMap();
        CreateMap<LayoutCoord, LayoutCoordModel>().ReverseMap();
    }
}