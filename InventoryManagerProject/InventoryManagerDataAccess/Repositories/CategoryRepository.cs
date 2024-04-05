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
            Category existingCategory = _context.Categories.FirstOrDefault(c => c.Id == category.Id);
            if (existingCategory != null)
            {
                throw new ArgumentException("Category id already exists!");
            }

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
            if (category == null)
            {
                throw new ArgumentException("Invalid category id!");
            }

            return category;
        }

        public void Update(int id, Category updatedCategory)
        {
            Category categoryToUpdate = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (categoryToUpdate == null)
            {
                throw new ArgumentException("Invalid category id!");
            }
            
            categoryToUpdate.Name = updatedCategory.Name;
            categoryToUpdate.Description = updatedCategory.Description;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Category category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                throw new ArgumentException("Invalid category id!");
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}
