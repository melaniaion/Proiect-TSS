using InventoryManagerDataAccess.Entities;
using InventoryManagerDataAccess.Interfaces;

namespace InventoryManagerDataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly InventoryManagerDbContext _context;
        public CategoryRepository(InventoryManagerDbContext context)
        {
            _context = context;
        }

        public int Create(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            int categoryId = category.Id;

            return categoryId;
        }

        public List<Category> GetAll()
        {
            List<Category> categories = _context.Categories.ToList();
            return categories;
        }

        public Category Get(int id)
        {
            Category category = _context.Categories.FirstOrDefault(c => c.Id == id);
            return category;
        }

        public void Update(int id, Category updatedCategory, Category categoryToUpdate)
        {
            categoryToUpdate.Name = updatedCategory.Name;
            categoryToUpdate.Description = updatedCategory.Description;

            _context.SaveChanges();
        }

        public void Delete(int id, Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}
