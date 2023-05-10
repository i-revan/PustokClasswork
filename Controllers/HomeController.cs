using Microsoft.AspNetCore.Mvc;

namespace PustokClassWork.Controllers
{
    [Area("PustokAdmin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
