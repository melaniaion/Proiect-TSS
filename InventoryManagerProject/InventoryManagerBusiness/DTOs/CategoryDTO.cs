using System.ComponentModel.DataAnnotations;

namespace InventoryManagerBusiness.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name should have at least 3 characters and should not exceed 100.")]
        public string Name { get; set; }

        [StringLength(200, MinimumLength = 3, ErrorMessage = "Description should have at least 3 characters and should not exceed 200.")]
        public string? Description { get; set; }
    }
}
