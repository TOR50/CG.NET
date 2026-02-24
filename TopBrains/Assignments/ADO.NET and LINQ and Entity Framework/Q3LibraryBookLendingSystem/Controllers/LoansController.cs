using LibraryLendingSystem.Data;
using LibraryLendingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryLendingSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoansController(LibraryDbContext context) : ControllerBase
{
    [HttpPost("borrow")]
    public async Task<IActionResult> BorrowBook([FromBody] LoanRequest request)
    {
        var book = await context.Books.FindAsync(request.BookId);
        if (book == null || !book.IsAvailable)
            return BadRequest("Book is not available.");

        var member = await context.Members.FindAsync(request.MemberId);
        if (member == null)
            return NotFound("Member not found.");

        var loan = new Loan
        {
            BookId = request.BookId,
            MemberId = request.MemberId,
            LoanDate = DateTime.UtcNow
        };

        book.IsAvailable = false; // Update availability

        context.Loans.Add(loan);
        await context.SaveChangesAsync();

        return Ok(loan);
    }

    [HttpPost("{loanId}/return")]
    public async Task<IActionResult> ReturnBook(int loanId)
    {
        var loan = await context.Loans.Include(l => l.Book).FirstOrDefaultAsync(l => l.Id == loanId);
        if (loan == null || loan.ReturnDate != null)
            return BadRequest("Invalid loan record or book already returned.");

        loan.ReturnDate = DateTime.UtcNow;
        if (loan.Book != null)
        {
            loan.Book.IsAvailable = true; // Update availability
        }

        await context.SaveChangesAsync();
        return Ok("Book returned successfully.");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllLoans()
    {
        var loans = await context.Loans
            .Include(l => l.Book)
            .Include(l => l.Member)
            .ToListAsync();
        return Ok(loans);
    }

    [HttpGet("overdue")]
    public async Task<IActionResult> GetOverdueLoans()
    {
        var overdueThreshold = DateTime.UtcNow.AddDays(-14);

        // LINQ: Filter loans by overdue status (older than 14 days and not returned)
        var overdueLoans = await context.Loans
            .Include(l => l.Book)
            .Include(l => l.Member)
            .Where(l => l.ReturnDate == null && l.LoanDate < overdueThreshold)
            .ToListAsync();

        return Ok(overdueLoans);
    }
}

// DTO for borrowing request
public class LoanRequest
{
    public int BookId { get; set; }
    public int MemberId { get; set; }
}