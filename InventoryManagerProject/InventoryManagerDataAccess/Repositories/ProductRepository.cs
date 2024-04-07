using InventoryManagerDataAccess.Entities;
using InventoryManagerDataAccess.Interfaces;

namespace InventoryManagerDataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly InventoryManagerDbContext _context;
        public ProductRepository(InventoryManagerDbContext context)
        {
            _context = context;
        }

        public int Create(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            int productId = product.Id;

            return productId;
        }

        public List<Product> GetAll()
        {
            List<Product> products = _context.Products.ToList();
            return products;
        }

        public Product Get(int id)
        {
            Product product = _context.Products.FirstOrDefault(p => p.Id == id);
            return product;
        }

        public void Update(Product updatedProduct, Product productToUpdate)
        {
            productToUpdate.Name = updatedProduct.Name;
            productToUpdate.Description = updatedProduct.Description;
            productToUpdate.Stock = updatedProduct.Stock;
            productToUpdate.Price = updatedProduct.Price;
            productToUpdate.Discount = updatedProduct.Discount;
            productToUpdate.CategoryId = updatedProduct.CategoryId;

            _context.SaveChanges();
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}
