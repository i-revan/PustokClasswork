using Microsoft.AspNetCore.Mvc;

namespace PustokClassWork.Areas.PustokAdmin.Controllers
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
