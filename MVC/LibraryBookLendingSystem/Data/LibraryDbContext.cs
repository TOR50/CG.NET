using LibraryLendingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryLendingSystem.Data;

public class LibraryDbContext(DbContextOptions<LibraryDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Member> Members => Set<Member>();
    public DbSet<Loan> Loans => Set<Loan>();
}