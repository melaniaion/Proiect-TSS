using InventoryManagerBusiness.Mappers;
using InventoryManagerDataAccess;
using InventoryManagerDataAccess.Interfaces;    
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagerBusiness.Extensions
{
    public static class ServiceExtension
    {
        public static void AddMyDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<InventoryManagerDbContext>(options => options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, ICategoryRepository>();
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CategoryProfile));
        }
    }
}
