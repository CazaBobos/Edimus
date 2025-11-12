using AutoMapper;
using Shared.Core.Entities;
using System.Text.Json;
using Tables.Core.Model;

namespace Tables.Core.Mapping;

public class TableProfile : Profile
{
    public TableProfile()
    {
        CreateMap<Table, TableModel>().ReverseMap();
        CreateMap<TableCoord, TableCoordModel>().ReverseMap();
    }
}