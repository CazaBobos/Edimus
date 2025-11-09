using AutoMapper;
using Products.Core.Model;
using Shared.Core.Entities;

namespace Products.Core.Mapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductModel>().ReverseMap();
    }
}