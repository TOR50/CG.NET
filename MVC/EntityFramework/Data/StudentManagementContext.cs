using EFcodefirst.Models;
using Microsoft.EntityFrameworkCore;

namespace EFcodeFirst.Data
{
    public class StudentManagementContext : DbContext
    {
        public StudentManagementContext(DbContextOptions<StudentManagementContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }


    }
}