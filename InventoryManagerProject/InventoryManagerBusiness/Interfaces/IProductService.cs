using InventoryManagerBusiness.DTOs;

namespace InventoryManagerBusiness.Interfaces
{
    public interface IProductService
    {
        int Create(ProductRequest productDto);
        List<ProductResponse> Get();
        ProductResponse Get(int id);
        List<ProductResponse> GetByCategory(int categoryId);
        void Update(int id, ProductRequest updatedProductDto);
        void Delete(int id);
    }
}
