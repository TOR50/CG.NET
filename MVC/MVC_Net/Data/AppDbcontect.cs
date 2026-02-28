using Microsoft.EntityFrameworkCore;

namespace mvc_net.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<mvc_net.Models.Student> Students { get; set; }
    }
}