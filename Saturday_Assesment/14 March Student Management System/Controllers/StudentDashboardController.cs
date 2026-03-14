using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSys.Data;
using StudentManagementSys.Models;
using StudentManagementSys.ViewModels;
using System.Linq;
using System.Security.Claims;

namespace StudentManagementSys.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentDashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentDashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var student = _context.Students
                .Include(s => s.Department)
                .Include(s => s.Course)
                .FirstOrDefault(s => s.Email == email);

            if (student == null)
            {
                return NotFound();
            }

            var model = new StudentProfileViewModel
            {
                StudentId = student.StudentId,
                Name = student.StudentName,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                Address = student.Address,
                DepartmentName = student.Department?.DepartmentName,
                CourseName = student.Course?.CourseName,
                Duration = student.Course?.Duration ?? 0,
                Fees = student.Course?.Fees ?? 0
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Profile(StudentProfileViewModel model)
        {
            // Update Phone number and Address
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var student = _context.Students.FirstOrDefault(s => s.Email == email && s.StudentId == model.StudentId);
            
            if (student != null)
            {
                student.PhoneNumber = model.PhoneNumber;
                student.Address = model.Address;
                
                _context.Students.Update(student);
                _context.SaveChanges();

                return RedirectToAction(nameof(Profile));
            }
            
            return View(model);
        }
    }
}
