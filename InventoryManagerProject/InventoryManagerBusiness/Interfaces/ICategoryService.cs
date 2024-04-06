

using InventoryManagerBusiness.DTOs;

namespace InventoryManagerBusiness.Interfaces
{
    public interface ICategoryService
    {
        int Create(CategoryRequest categoryDTO);
        List<CategoryResponse> Get();
        CategoryResponse Get(int id);
        void Update(int id, CategoryRequest updatedCategoryDTO);
        void Delete(int id);
    }
}
