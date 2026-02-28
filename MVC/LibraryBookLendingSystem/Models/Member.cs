using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryLendingSystem.Models;

public class Member
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [JsonIgnore]
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}