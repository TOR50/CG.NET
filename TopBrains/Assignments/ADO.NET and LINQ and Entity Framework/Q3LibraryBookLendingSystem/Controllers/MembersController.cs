using LibraryLendingSystem.Data;
using LibraryLendingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryLendingSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MembersController(LibraryDbContext context) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> RegisterMember(Member member)
    {
        context.Members.Add(member);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetMember), new { id = member.Id }, member);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMember(int id) =>
        await context.Members.FindAsync(id) is Member member ? Ok(member) : NotFound();

    [HttpGet]
    public async Task<IActionResult> GetAllMembers() =>
        Ok(await context.Members.ToListAsync());

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMember(int id, Member member)
    {
        if (id != member.Id) return BadRequest();
        context.Entry(member).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMember(int id)
    {
        var member = await context.Members.FindAsync(id);
        if (member == null) return NotFound();
        context.Members.Remove(member);
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("{id}/active-loans")]
    public async Task<IActionResult> GetActiveLoans(int id)
    {
        // LINQ: Get all active loans for a specific member
        var activeLoans = await context.Loans
            .Include(l => l.Book)
            .Where(l => l.MemberId == id && l.ReturnDate == null)
            .ToListAsync();

        return Ok(activeLoans);
    }
}