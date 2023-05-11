using Microsoft.EntityFrameworkCore;
using PustokClassWork.DAL;

namespace PustokClassWork
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });
            var app = builder.Build();
            app.UseStaticFiles();
            app.MapControllerRoute(
                name: "default",
                pattern: "{area:exists}/{controller=home}/{action=index}/{id?}"
                );
            app.MapControllerRoute(
                name:"default",
                pattern: "{controller=home}/{action=index}/{id?}"
                );

            app.Run();
        }
    }
}