using AutoMapper;
using InventoryManagerBusiness.DTOs;
using InventoryManagerDataAccess.Entities;

namespace InventoryManagerBusiness.Mappers
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryRequest, Category>();
            CreateMap<Category, CategoryResponse>();
        }
    }
}
