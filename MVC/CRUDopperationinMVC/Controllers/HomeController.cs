using Microsoft.AspNetCore.Mvc;

namespace CREDopperation.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Student");
        }
    }
}
