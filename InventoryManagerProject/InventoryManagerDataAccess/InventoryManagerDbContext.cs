using InventoryManagerDataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagerDataAccess
{
    public class InventoryManagerDbContext : DbContext
    {
        public InventoryManagerDbContext(DbContextOptions<InventoryManagerDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .HasIndex(p => p.Name)
                .IsUnique();
        }
    }
}