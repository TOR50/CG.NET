using Microsoft.EntityFrameworkCore;
using CollegeApplication.Models;

namespace CollegeApplication.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationForm> ApplicationForms { get; set; }
    }
}
