using AutoMapper;
using Sectors.Core.Model;
using Shared.Core.Entities;
using System.Drawing;

namespace Sectors.Core.Mapping;

public class SectorProfile : Profile
{
    public SectorProfile()
    {
        CreateMap<Sector, SectorModel>()
            .ForMember(e => e.Surface, config => config.MapFrom(e => e.Surface.Select(e => new[] { e.X, e.Y })))
            .ReverseMap();
        CreateMap<SectorCoord, SectorCoordModel>().ReverseMap();
    }
}