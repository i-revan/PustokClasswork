using Microsoft.AspNetCore.Mvc;
using PustokClassWork.DAL;
using PustokClassWork.Models;

namespace PustokClassWork.Areas.PustokAdmin.Controllers
{
    [Area("PustokAdmin")]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Employee> employees = _context.Employees.ToList();
            return View(employees);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Update(int? id)
        {
            Employee employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            return View(employee);
        }
        [HttpPost]
        public IActionResult Update(int? id,Employee emp)
        {
            return View();
        }

    }
}
