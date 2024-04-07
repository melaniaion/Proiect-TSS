using InventoryManagerDataAccess.Entities;

namespace InventoryManagerDataAccess.Interfaces
{
    public interface ICategoryRepository
    {
        public int Create(Category category);
        public List<Category> GetAll();
        public Category Get(int id);
        public void Update(Category updatedCategory, Category categoryToUpdate);
        public void Delete(Category category);
    }
}
