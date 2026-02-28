using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EnrollmentSystem.Data;
using EnrollmentSystem.Models;

namespace EnrollmentSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EnrollmentsController : ControllerBase
{
    private readonly EnrollmentDbContext _context;

    public EnrollmentsController(EnrollmentDbContext context)
    {
        _context = context;
    }

    // GET: api/enrollments
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetEnrollments()
    {
        var enrollments = await _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .Select(e => new
            {
                e.Id,
                e.StudentId,
                StudentName = e.Student.Name,
                e.CourseId,
                CourseTitle = e.Course.Title,
                e.EnrollmentDate
            })
            .ToListAsync();

        return Ok(enrollments);
    }

    // POST: api/enrollments
    [HttpPost]
    public async Task<ActionResult<Enrollment>> PostEnrollment(Enrollment enrollment)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var studentExists = await _context.Students.AnyAsync(s => s.Id == enrollment.StudentId);
        if (!studentExists)
        {
            return NotFound(new { message = $"Student with ID {enrollment.StudentId} not found" });
        }

        var courseExists = await _context.Courses.AnyAsync(c => c.Id == enrollment.CourseId);
        if (!courseExists)
        {
            return NotFound(new { message = $"Course with ID {enrollment.CourseId} not found" });
        }

        var existingEnrollment = await _context.Enrollments
            .FirstOrDefaultAsync(e => e.StudentId == enrollment.StudentId && e.CourseId == enrollment.CourseId);

        if (existingEnrollment != null)
        {
            return Conflict(new { message = "Student is already enrolled in this course" });
        }

        enrollment.EnrollmentDate = DateTime.UtcNow;
        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();

        var createdEnrollment = await _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .Where(e => e.Id == enrollment.Id)
            .Select(e => new
            {
                e.Id,
                e.StudentId,
                StudentName = e.Student.Name,
                e.CourseId,
                CourseTitle = e.Course.Title,
                e.EnrollmentDate
            })
            .FirstOrDefaultAsync();

        return CreatedAtAction(nameof(GetEnrollments), new { id = enrollment.Id }, createdEnrollment);
    }

    // GET: api/enrollments/student/5
    [HttpGet("student/{studentId}")]
    public async Task<ActionResult<IEnumerable<object>>> GetEnrollmentsByStudent(int studentId)
    {
        var studentExists = await _context.Students.AnyAsync(s => s.Id == studentId);
        if (!studentExists)
        {
            return NotFound(new { message = $"Student with ID {studentId} not found" });
        }

        var enrollments = await _context.Enrollments
            .Include(e => e.Course)
            .Where(e => e.StudentId == studentId)
            .Select(e => new
            {
                e.Id,
                e.CourseId,
                CourseTitle = e.Course.Title,
                CourseDescription = e.Course.Description,
                CourseDuration = e.Course.Duration,
                e.EnrollmentDate
            })
            .ToListAsync();

        return Ok(enrollments);
    }

    // GET: api/enrollments/course/5
    [HttpGet("course/{courseId}")]
    public async Task<ActionResult<IEnumerable<object>>> GetEnrollmentsByCourse(int courseId)
    {
        var courseExists = await _context.Courses.AnyAsync(c => c.Id == courseId);
        if (!courseExists)
        {
            return NotFound(new { message = $"Course with ID {courseId} not found" });
        }

        var enrollments = await _context.Enrollments
            .Include(e => e.Student)
            .Where(e => e.CourseId == courseId)
            .Select(e => new
            {
                e.Id,
                e.StudentId,
                StudentName = e.Student.Name,
                StudentEmail = e.Student.Email,
                e.EnrollmentDate
            })
            .ToListAsync();

        return Ok(enrollments);
    }
}
