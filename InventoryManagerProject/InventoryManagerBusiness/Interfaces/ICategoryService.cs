

using InventoryManagerBusiness.DTOs;

namespace InventoryManagerBusiness.Interfaces
{
    public interface ICategoryService
    {
        int Create(CategoryDTO categoryDTO);
        List<CategoryDTO> Get();
        CategoryDTO Get(int id);
        void Update(int id, CategoryDTO updatedCategoryDTO);
        void Delete(int id);
    }
}
