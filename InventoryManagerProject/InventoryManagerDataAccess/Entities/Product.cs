using System.ComponentModel.DataAnnotations;

namespace InventoryManagerDataAccess.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        [Range(1, 100000)]
        public int Stock { get; set; }

        [Required]
        [Range(0, 1000000.00)]
        public double Price { get; set; }

        [Required]
        [Range(0, 100)]
        public int Discount { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
