using System.ComponentModel.DataAnnotations;

namespace EnrollmentSystem.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

        public Student Student { get; set; } = null!;
        public Course Course { get; set; } = null!;
    }
}
