using InventoryManagerDataAccess;
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

    }
}
