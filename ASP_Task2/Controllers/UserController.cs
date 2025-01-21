using Microsoft.AspNetCore.Mvc;

namespace ASP_Task2.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
