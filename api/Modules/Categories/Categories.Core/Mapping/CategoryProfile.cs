using AutoMapper;
using Categories.Core.Model;
using Shared.Core.Entities;

namespace Categories.Core.Mapping;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryModel>()
            .ReverseMap();
    }
}