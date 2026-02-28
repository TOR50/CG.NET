using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EnrollmentSystem.Data;
using EnrollmentSystem.Models;

namespace EnrollmentSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly EnrollmentDbContext _context;

    public StudentsController(EnrollmentDbContext context)
    {
        _context = context;
    }

    // GET: api/students
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        return await _context.Students.ToListAsync();
    }

    // GET: api/students/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);

        if (student == null)
        {
            return NotFound(new { message = $"Student with ID {id} not found" });
        }

        return student;
    }

    // POST: api/students
    [HttpPost]
    public async Task<ActionResult<Student>> PostStudent(Student student)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingStudent = await _context.Students
            .FirstOrDefaultAsync(s => s.Email == student.Email);

        if (existingStudent != null)
        {
            return Conflict(new { message = "A student with this email already exists" });
        }

        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
    }

    // PUT: api/students/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutStudent(int id, Student student)
    {
        if (id != student.Id)
        {
            return BadRequest(new { message = "ID mismatch" });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingStudent = await _context.Students
            .FirstOrDefaultAsync(s => s.Email == student.Email && s.Id != id);

        if (existingStudent != null)
        {
            return Conflict(new { message = "A student with this email already exists" });
        }

        _context.Entry(student).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await StudentExists(id))
            {
                return NotFound(new { message = $"Student with ID {id} not found" });
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/students/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound(new { message = $"Student with ID {id} not found" });
        }

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> StudentExists(int id)
    {
        return await _context.Students.AnyAsync(e => e.Id == id);
    }
}
