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
            CreateMap<Product, ProductResponse>();
        }
    }
}
