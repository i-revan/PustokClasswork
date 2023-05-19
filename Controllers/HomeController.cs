using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokClassWork.DAL;
using PustokClassWork.Models;
using PustokClassWork.ViewModels.Home;

namespace PustokClassWork.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _context.Sliders.ToListAsync();
            List<Product> products= await _context.Products.ToListAsync();
            HomeVM homeVM = new HomeVM()
            {
                sliders= sliders,
                products=products
            };
            return View(homeVM);
        }
    }
}
