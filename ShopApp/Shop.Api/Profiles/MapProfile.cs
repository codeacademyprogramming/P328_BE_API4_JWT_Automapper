using AutoMapper;
using Shop.Api.Dtos.BrandDtos;
using Shop.Api.Dtos.ProductDtos;
using Shop.Core.Entities;

namespace Shop.Api.Profiles
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Product, ProductGetDto>();
            CreateMap<ProductPostDto, Product>();
            CreateMap<Product, ProductGetAllItemDto>()
                .ForMember(dest => dest.HasDiscount, m => m.MapFrom(s => s.DiscountPercent > 0));

            CreateMap<BrandPostDto, Brand>();
            CreateMap<Brand,BrandGetAllItemDto>();
            CreateMap<Brand,BrandInProductGetDto>();
        }
    }
}
