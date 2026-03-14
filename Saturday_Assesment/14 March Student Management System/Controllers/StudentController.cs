using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagementSys.Data;
using StudentManagementSys.Models;
using System.Linq;

namespace StudentManagementSys.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchString, int? deptId)
        {
            var students = _context.Students.Include(s => s.Department).Include(s => s.Course).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.StudentName.Contains(searchString));
            }

            if (deptId.HasValue)
            {
                students = students.Where(s => s.DepartmentId == deptId.Value);
            }

            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentId", "DepartmentName");
            return View(students.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentId", "DepartmentName");
            ViewBag.Courses = new SelectList(_context.Courses, "CourseId", "CourseName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", student.DepartmentId);
            ViewBag.Courses = new SelectList(_context.Courses, "CourseId", "CourseName", student.CourseId);
            return View(student);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null) return NotFound();

            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", student.DepartmentId);
            ViewBag.Courses = new SelectList(_context.Courses, "CourseId", "CourseName", student.CourseId);
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Update(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", student.DepartmentId);
            ViewBag.Courses = new SelectList(_context.Courses, "CourseId", "CourseName", student.CourseId);
            return View(student);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var student = _context.Students.Include(s => s.Department).Include(s => s.Course).FirstOrDefault(s => s.StudentId == id);
            if (student == null) return NotFound();
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = _context.Students.Find(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
