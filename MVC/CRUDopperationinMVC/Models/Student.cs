using System.ComponentModel.DataAnnotations;

namespace CREDopperation.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(5, 100, ErrorMessage = "Age must be between 5 and 100")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Marks are required")]
        [Range(0, 100, ErrorMessage = "Marks must be between 0 and 100")]
        public decimal Marks { get; set; }
    }
}
