using AutoMapper;
using Sectors.Core.Model;
using Shared.Core.Entities;

namespace Sectors.Core.Mapping;

public class SectorProfile : Profile
{
    public SectorProfile()
    {
        CreateMap<Sector, SectorModel>().ReverseMap();
        CreateMap<SectorCoord, SectorCoordModel>().ReverseMap();
    }
}