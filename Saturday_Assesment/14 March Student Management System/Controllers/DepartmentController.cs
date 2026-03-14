using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSys.Data;
using StudentManagementSys.Models;
using System.Linq;

namespace StudentManagementSys.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var depts = _context.Departments.ToList();
            return View(depts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department dept)
        {
            if (ModelState.IsValid)
            {
                _context.Departments.Add(dept);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(dept);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var dept = _context.Departments.Find(id);
            if (dept == null) return NotFound();
            return View(dept);
        }

        [HttpPost]
        public IActionResult Edit(Department dept)
        {
            if (ModelState.IsValid)
            {
                _context.Departments.Update(dept);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(dept);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var dept = _context.Departments.Find(id);
            if (dept == null) return NotFound();
            return View(dept);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var dept = _context.Departments.Find(id);
            if (dept != null)
            {
                _context.Departments.Remove(dept);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
