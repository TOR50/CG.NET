using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mvc.Models;
using mvc.Data;

namespace mvc.Controllers
{
    public class HomeController : Controller

    {
        private readonly ILogger<HomeController> _logger;
        private readonly StudentRepository _studentRepository;

        public HomeController(ILogger<HomeController> logger, StudentRepository studentRepository)
        {
            _logger = logger;
            _studentRepository = studentRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AddNumbers()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNumbers(int number1, int number2)
        {
            int sum = number1 + number2;
            ViewBag.Result = sum;
            ViewBag.Number1 = number1;
            ViewBag.Number2 = number2;
            return View();
        }

        public IActionResult Students()
        {
            var students = _studentRepository.GetAllStudents();
            return View(students);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
