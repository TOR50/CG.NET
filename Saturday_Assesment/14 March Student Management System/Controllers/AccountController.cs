using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSys.Data;
using StudentManagementSys.Models;
using StudentManagementSys.ViewModels;
using System.Security.Claims;
using System.Linq;

namespace StudentManagementSys.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userExists = _context.Users.Any(u => u.Email == model.Email);
                if (userExists)
                {
                    ModelState.AddModelError("", "Email already exists");
                    return View(model);
                }

                var user = new User
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    Password = model.Password, // Simple text to keep it simple as per instructions, no hashing required in the prompt
                    Role = model.Role
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    var userClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.FullName),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Role)
                    };

                    var identity = new ClaimsIdentity(userClaims, "CookieAuth");
                    var userPrincipal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync("CookieAuth", userPrincipal).Wait();

                    if (user.Role == "Teacher")
                    {
                        return RedirectToAction("Index", "TeacherDashboard");
                    }
                    else if (user.Role == "Student")
                    {
                        // make sure we have corresponding student record 
                        var studentExists = _context.Students.Any(s => s.Email == user.Email);
                        if (!studentExists)
                        {
                            // we probably need a dummy redirect if haven't config
                        }
                        return RedirectToAction("Index", "StudentDashboard");
                    }
                }
                ModelState.AddModelError("", "Invalid Login Credentials");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("CookieAuth").Wait();
            return RedirectToAction("Login");
        }
    }
}
