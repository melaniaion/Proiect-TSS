using AutoMapper;
using InventoryManagerBusiness.DTOs;
using InventoryManagerDataAccess.Entities;

namespace InventoryManagerBusiness.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductRequest, Product>();
            CreateMap<Product, ProductResponse>()
               .ForMember(dest => dest.FullPrice,
                    opt => opt.MapFrom(src => src.Price))
               .ForMember(dest => dest.DiscountedPrice,
                    opt => opt.MapFrom(src => src.Price - (src.Price * src.Discount / 100)))
               .ForMember(dest => dest.Discount,
                    opt => opt.MapFrom(src => src.Discount));
        }
    }
}
