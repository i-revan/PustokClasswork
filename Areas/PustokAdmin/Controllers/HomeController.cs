using Microsoft.AspNetCore.Mvc;

namespace PustokClassWork.Areas.PustokAdmin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
