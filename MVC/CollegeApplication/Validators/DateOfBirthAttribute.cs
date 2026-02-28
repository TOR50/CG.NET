using System.ComponentModel.DataAnnotations;

namespace CollegeApplication.Validators
{
   
    public class DateOfBirthAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;
        private readonly int _maximumAge;

        public DateOfBirthAttribute(int minimumAge = 15, int maximumAge = 50)
        {
            _minimumAge = minimumAge;
            _maximumAge = maximumAge;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Date of Birth is required.");
            }

            if (value is DateTime dateOfBirth)
            {
                var today = DateTime.Today;

                if (dateOfBirth > today)
                {
                    return new ValidationResult("Date of Birth cannot be in the future.");
                }

                int age = today.Year - dateOfBirth.Year;
                
                if (dateOfBirth.Date > today.AddYears(-age))
                {
                    age--;
                }

                if (age < _minimumAge)
                {
                    return new ValidationResult($"You must be at least {_minimumAge} years old to apply.");
                }

                if (age > _maximumAge)
                {
                    return new ValidationResult($"Age cannot exceed {_maximumAge} years.");
                }

                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid Date of Birth format.");
        }
    }
}
