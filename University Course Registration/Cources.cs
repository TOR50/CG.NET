using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Course_Registration_System
{
    // =========================
    // Course Class
    // =========================
    public class Course
    {
        public string CourseCode { get; private set; }
        public string CourseName { get; private set; }
        public int Credits { get; private set; }
        public int MaxCapacity { get; private set; }
        public List<string> Prerequisites { get; private set; }

        private int CurrentEnrollment;

        public Course(string code, string name, int credits, int maxCapacity = 50, List<string> prerequisites = null)
        {
            CourseCode = code;
            CourseName = name;
            Credits = credits;
            MaxCapacity = maxCapacity;
            Prerequisites = prerequisites ?? new List<string>();
            CurrentEnrollment = 0;
        }
        public int GetCurrentEnrollment()
        {
            return CurrentEnrollment;
        }
        public bool IsFull()
        {
            return CurrentEnrollment >= MaxCapacity;
        }

        public bool HasPrerequisites(List<string> completedCourses)
        {
            if (Prerequisites.Count == 0) return true;
            return Prerequisites.All(req => completedCourses.Any(done => done.Equals(req, StringComparison.OrdinalIgnoreCase)));
        }

        public void EnrollStudent()
        {
            if (IsFull())
            {
                throw new InvalidOperationException($"Course {CourseCode} is full.");
            }
            CurrentEnrollment++;
        }

        public void DropStudent()
        {
            if (CurrentEnrollment > 0)
            {
                CurrentEnrollment--;
            }
        }

        public string GetEnrollmentInfo()
        {
            return $"{CurrentEnrollment}/{MaxCapacity}";
        }
    }
}
