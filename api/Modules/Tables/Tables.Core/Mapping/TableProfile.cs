using AutoMapper;
using Shared.Core.Entities;
using System.Text.Json;
using Tables.Core.Model;

namespace Tables.Core.Mapping;

public class TableProfile : Profile
{
    private List<(int, int)> DeserializeSurface(string json) => JsonSerializer.Deserialize<List<(int, int)>>(json) ?? new();
    public TableProfile()
    {
        CreateMap<Table, TableModel>()
            .ForMember(model => model.Surface, config => config.MapFrom(e => DeserializeSurface(e.Surface)))
            .ReverseMap();
    }
}