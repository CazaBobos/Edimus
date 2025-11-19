using AutoMapper;
using Shared.Core.Entities;
using Tables.Core.Model;

namespace Tables.Core.Mapping;

public class TableProfile : Profile
{
    public TableProfile()
    {
        CreateMap<Table, TableModel>().ReverseMap();
        CreateMap<TableCoord, TableCoordModel>().ReverseMap();
        CreateMap<Request, RequestModel>().ReverseMap();
    }
}