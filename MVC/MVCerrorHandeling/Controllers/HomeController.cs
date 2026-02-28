using Microsoft.AspNetCore.Mvc;
using mvcErrorHandeling.Models;
using System.Diagnostics;

namespace mvcErrorHandeling.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult StudentInfo()
        {
            ViewData["Name"] = "Rauhan";
            ViewData["CollegeName"] = "LPU";
            return View();
        }
        //public IActionResult TestError()
        //{
        //    int x = 10;
        //    int y = 0;
        //    int res = x / y;
        //    return Content(res.ToString());
        //}

        //public IActionResult Error()
        //{   
        //    return View();
        //}



    }
}
