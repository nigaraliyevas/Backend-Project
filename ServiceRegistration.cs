using EduHome.DAL;
using Microsoft.EntityFrameworkCore;

namespace EduHome
{
    public static class ServiceRegistration
    {
        public static void Register(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllersWithViews();
            services.AddDbContext<EduHomeDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            //services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromSeconds(20);
            //});
            services.AddHttpContextAccessor();
        }
    }
}
