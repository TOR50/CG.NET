using LibraryLendingSystem.Data;
using LibraryLendingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryLendingSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController(LibraryDbContext context) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddBook(Book book)
    {
        context.Books.Add(book);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBook(int id) =>
        await context.Books.FindAsync(id) is Book book ? Ok(book) : NotFound();

    [HttpGet]
    public async Task<IActionResult> GetAllBooks([FromQuery] bool? availableOnly)
    {
        // LINQ: Get all books currently available
        var query = context.Books.AsQueryable();
        if (availableOnly.HasValue && availableOnly.Value)
        {
            query = query.Where(b => b.IsAvailable);
        }
        return Ok(await query.ToListAsync());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, Book updatedBook)
    {
        if (id != updatedBook.Id) return BadRequest();
        context.Entry(updatedBook).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await context.Books.FindAsync(id);
        if (book == null) return NotFound();
        context.Books.Remove(book);
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("top-borrowed")]
    public async Task<IActionResult> GetTopBorrowedBooks()
    {
        // LINQ: Get top 5 most borrowed books
        var topBooks = await context.Loans
            .GroupBy(l => l.BookId)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => new { BookId = g.Key, BorrowCount = g.Count() })
            .Join(context.Books, l => l.BookId, b => b.Id, (l, b) => new
            {
                b.Id,
                b.Title,
                b.Author,
                l.BorrowCount
            })
            .ToListAsync();

        return Ok(topBooks);
    }
}