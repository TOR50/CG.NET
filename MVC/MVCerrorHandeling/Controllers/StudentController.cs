using Microsoft.AspNetCore.Mvc;

namespace mvcErrorHandeling.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Receive()
        {
            var msg = TempData["Message"];
            return Content(msg?.ToString());
        }
    }
}
