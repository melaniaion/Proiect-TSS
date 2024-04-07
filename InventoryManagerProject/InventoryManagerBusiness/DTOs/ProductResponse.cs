
namespace InventoryManagerBusiness.DTOs
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public double DiscountedPrice => FullPrice - (FullPrice * Discount / 100);
        public int Discount { get; set; }
        public double FullPrice { get; set; }
        public int CategoryId { get; set; }
    }
}
