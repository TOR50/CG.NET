using System.ComponentModel.DataAnnotations;
using CollegeApplication.Validators;

namespace CollegeApplication.Models
{
    public class ApplicationForm
    {
        [Key]
        public int ApplicationId { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Father's Name is required")]
        [StringLength(100, ErrorMessage = "Father's Name cannot exceed 100 characters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Father's Name can only contain letters and spaces")]
        [Display(Name = "Father's Name")]
        public string FatherName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mother's Name is required")]
        [StringLength(100, ErrorMessage = "Mother's Name cannot exceed 100 characters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Mother's Name can only contain letters and spaces")]
        [Display(Name = "Mother's Name")]
        public string MotherName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Please enter a valid email address")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone Number is required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone Number must be 10 digits")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required")]
        [StringLength(250, ErrorMessage = "Address cannot exceed 250 characters")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required")]
        [StringLength(50, ErrorMessage = "City cannot exceed 50 characters")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "State is required")]
        [StringLength(50, ErrorMessage = "State cannot exceed 50 characters")]
        public string State { get; set; } = string.Empty;

        [Required(ErrorMessage = "Pincode is required")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Pincode must be 6 digits")]
        public string Pincode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Course is required")]
        [Display(Name = "Course Applied For")]
        public string Course { get; set; } = string.Empty;

        [Required(ErrorMessage = "10th Percentage is required")]
        [Range(0, 100, ErrorMessage = "Percentage must be between 0 and 100")]
        [Display(Name = "10th Percentage")]
        public decimal TenthPercentage { get; set; }

        [Required(ErrorMessage = "12th Percentage is required")]
        [Range(0, 100, ErrorMessage = "Percentage must be between 0 and 100")]
        [Display(Name = "12th Percentage")]
        public decimal TwelfthPercentage { get; set; }

        [Display(Name = "Application Date")]
        public DateTime ApplicationDate { get; set; } = DateTime.Now;
    }
}
