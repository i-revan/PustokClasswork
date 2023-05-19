using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PustokClassWork.DAL;
using PustokClassWork.Models;
using PustokClassWork.ViewModels;
using System.Net;

namespace PustokClassWork.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext _context;

        public BasketController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            string json = Request.Cookies["Basket"];
            List<BasketCookiesItemsVM> basket;
            if (!String.IsNullOrEmpty(json))
            {
                basket = JsonConvert.DeserializeObject<List<BasketCookiesItemsVM>>(json);
            }
            else
            {
                basket= new List<BasketCookiesItemsVM>();
            }
            List<BasketItemVM> items = new List<BasketItemVM>();
            foreach(var item in basket)
            {
                Product product = await _context.Products.FirstOrDefaultAsync(p=>p.Id==item.Id);
                BasketItemVM itemVM = new BasketItemVM()
                {
                    Id= product.Id,
                    Title = product.Title,
                    Description= product.Description,
                    Price= product.Price,
                    Image = product.Image,
                    Count = item.Count
                };
                items.Add(itemVM);
            }
            
            return View(items);
        }
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id == null || id<1) return BadRequest();
            Product product = await _context.Products.FirstOrDefaultAsync(p=>p.Id==id);
            if (product == null) return NotFound();
            List<BasketCookiesItemsVM> basket;
            if (Request.Cookies["Basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketCookiesItemsVM>>(Request.Cookies["Basket"]);
                BasketCookiesItemsVM existed = basket.FirstOrDefault(p => p.Id==id);
                if (existed == null) 
                {
                    basket.Add(new BasketCookiesItemsVM()
                    {
                        Id = product.Id,
                        Count = 1
                    });
                }
                else
                {
                    existed.Count++;
                }
            }
            else
            {
                basket = new List<BasketCookiesItemsVM>();
                basket.Add(new BasketCookiesItemsVM()
                {
                    Id = product.Id,
                    Count = 1
                });
            }

            string json = JsonConvert.SerializeObject(basket);
            Response.Cookies.Append("Basket", json);
            return RedirectToAction("Index","Home");
        }
        public IActionResult GetBasket()
        {
            var basket = JsonConvert.DeserializeObject<List<BasketCookiesItemsVM>>(Request.Cookies["Basket"]);
            return Json(basket);
        }
    }
}
