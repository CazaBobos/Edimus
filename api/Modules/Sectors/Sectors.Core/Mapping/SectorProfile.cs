using AutoMapper;
using Sectors.Core.Model;
using Shared.Core.Entities;
using System.Drawing;

namespace Sectors.Core.Mapping;

public class SectorProfile : Profile
{
    public SectorProfile()
    {
        CreateMap<Sector, SectorModel>().ReverseMap();
        CreateMap<SectorCoord, SectorCoordModel>().ReverseMap();
    }
}