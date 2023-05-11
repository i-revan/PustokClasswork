using Microsoft.EntityFrameworkCore;
using PustokClassWork.Models;

namespace PustokClassWork.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
        {

        }
        public DbSet<Slider> Sliders { get; set; }
    }
}
