using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSys.Data;
using System.Linq;

namespace StudentManagementSys.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class TeacherDashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeacherDashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Bonus feature: total students per department
            var deptStats = _context.Departments.Select(d => new
            {
                DepartmentName = d.DepartmentName,
                StudentCount = d.Students.Count()
            }).ToList();

            ViewBag.DeptStats = deptStats;

            return View();
        }
    }
}
