using System.ComponentModel.DataAnnotations;

namespace InventoryManagerBusiness.DTOs
{
    public class ProductRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name should have at least 3 characters and should not exceed 50.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Description should have at least 3 characters and should not exceed 100.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Stock is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Stock should be greater than 0.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(1, double.MaxValue, ErrorMessage = "Price should be greater than 0.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Discount is required.")]
        [Range(0, 100, ErrorMessage = "Discount should be between 0 and 100")]
        public int Discount { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }
    }
}
