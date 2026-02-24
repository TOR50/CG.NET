using System.ComponentModel.DataAnnotations;

namespace LibraryLendingSystem.Models;

public class Loan
{
    public int Id { get; set; }

    [Required]
    public int BookId { get; set; }
    public Book? Book { get; set; }

    [Required]
    public int MemberId { get; set; }
    public Member? Member { get; set; }

    [Required]
    public DateTime LoanDate { get; set; } = DateTime.UtcNow;

    public DateTime? ReturnDate { get; set; }
}