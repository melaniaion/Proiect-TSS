﻿
using InventoryManagerDataAccess.Entities;

namespace InventoryManagerDataAccess.Interfaces
{
    public interface IProductRepository
    {
        public int Create(Product product);
        public List<Product> GetAll();
        public Product Get(int id);
        public List<Product> GetByCategory(int categoryId);
        public void Update(Product updatedProduct, Product productToUpdate);
        public void Delete(Product product);
    }
}
