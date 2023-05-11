using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokClassWork.DAL;
using PustokClassWork.Models;
using PustokClassWork.Utilities.Extensions;

namespace PustokClassWork.Areas.PustokAdmin.Controllers
{
    [Area("PustokAdmin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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
            if (!slider.Photo.CheckFileType("image/")) 
            {
                ModelState.AddModelError("Photo","Zehmet olmasa sekil atin!");
                return View();
            }
            if (!slider.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "Sekilin olcusu 200 kb'dan cox olmamalidir");
                return View();
            }
            slider.Image = await slider.Photo.CreateFileAsync(_env.WebRootPath, @"image/bg-images");
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1) return BadRequest();
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(s=>s.Id==id);
            if (slider == null) return NotFound();
            return View(slider);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id,Slider slider)
        {
            if (id == null || id < 1) return BadRequest();
            Slider existed = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (existed == null) return NotFound();
            if(slider.Photo != null)
            {
                if (!slider.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "Zehmet olmasa sekil tipinden fayl daxil edin");
                    return View();
                }
                if (!slider.Photo.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "Fayl hecmi 500 kb'dan cox olmamalidir");
                    return View();
                }
                existed.Image.DeleteFile(_env.WebRootPath, @"image/bg-images");
                existed.Image = await slider.Photo.CreateFileAsync(_env.WebRootPath, @"image/bg-images");
            }
            existed.ButtonDescription= slider.ButtonDescription;
            existed.Description = slider.Description;
            existed.BookName = slider.BookName;
            existed.AuthorName= slider.AuthorName;
            existed.BookPrice = slider.BookPrice;
            existed.Order = slider.Order;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider == null) return NotFound();
            slider.Image.DeleteFile(_env.WebRootPath, @"image/bg-images");
            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || id < 1) return BadRequest();
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider == null) return NotFound();
            return View(slider);
        }
    }
}
