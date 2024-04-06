using InventoryManagerDataAccess.Entities;

namespace InventoryManagerDataAccess.Interfaces
{
    public interface ICategoryRepository
    {
        public int Create(Category category);
        public List<Category> GetAll();
        public Category Get(int id);
        public void Update(int id, Category updatedCategory, Category categoryToUpdate);
        public void Delete(int id, Category category);
    }
}
