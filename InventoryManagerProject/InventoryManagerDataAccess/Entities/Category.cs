using System.ComponentModel.DataAnnotations;

namespace InventoryManagerDataAccess.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(200, MinimumLength = 3)]
        public string? Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
