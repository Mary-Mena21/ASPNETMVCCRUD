using Microsoft.AspNetCore.Mvc;

namespace ASPNETMVCCRUD.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
