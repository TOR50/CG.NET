using System.ComponentModel.DataAnnotations;

namespace StudentManagementSys.ViewModels
{
    public class StudentProfileViewModel
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        
        [Phone]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public string DepartmentName { get; set; }
        public string CourseName { get; set; }
        
        public int Duration { get; set; }
        public decimal Fees { get; set; }
    }
}
