using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokClassWork.DAL;
using PustokClassWork.Models;

namespace PustokClassWork.Areas.PustokAdmin.Controllers
{
    [Area("PustokAdmin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;

        public SliderController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _context.Sliders.ToListAsync();
            return View(sliders);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Slider slider) {
            if(slider.Photo == null) return View();
            if (!slider.Photo.ContentType.Contains("image/")) 
            {
                ModelState.AddModelError("Photo","Zehmet olmasa sekil atin!");
                return View();
            }
           
            FileStream file = new FileStream(@"C:\Users\ClassTime\Desktop\NS\Algorithm\PustokClassWork\wwwroot\image\bg-images\"+slider.Photo.FileName,FileMode.Create);
            await slider.Photo.CopyToAsync(file);
            slider.Image = slider.Photo.FileName;
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(s=>s.Id==id);
            return View(slider);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id,Slider slider)
        {
            return View();
        }
    }
}
