using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Course_Registration_System
{
    // =========================
    // University System Class
    // =========================
    public class UniversitySystem
    {
        public Dictionary<string, Course> AvailableCourses { get; private set; }
        public Dictionary<string, Student> Students { get; private set; }

        public UniversitySystem()
        {
            AvailableCourses = new Dictionary<string, Course>();
            Students = new Dictionary<string, Student>();
        }

        public void AddCourse(string code, string name, int credits, int maxCapacity = 50, List<string> prerequisites = null)
        {
            if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("Course code cannot be empty.");
            
            if (AvailableCourses.ContainsKey(code))
            {
                throw new ArgumentException($"Course code '{code}' already exists.");
            }

            Course newCourse = new Course(code, name, credits, maxCapacity, prerequisites);
            AvailableCourses.Add(newCourse.CourseCode, newCourse);
            Console.WriteLine($"Course {newCourse.CourseCode} added successfully.");
        }

        public void AddStudent(string id, string name, string major, int maxCredits = 18, List<string> completedCourses = null)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Student ID cannot be empty.");

            if (Students.ContainsKey(id))
            {
                throw new ArgumentException($"Student ID '{id}' already exists.");
            }

            Student newStudent = new Student(id, name, major, maxCredits, completedCourses);
            Students.Add(newStudent.StudentId, newStudent);
            Console.WriteLine($"Student {newStudent.StudentId} added successfully.");
        }

        public bool RegisterStudentForCourse(string studentId, string courseCode)
        {
            if (!Students.ContainsKey(studentId))
            {
                Console.WriteLine($"Error: Student ID {studentId} not found.");
                return false;
            }
            if (!AvailableCourses.ContainsKey(courseCode))
            {
                Console.WriteLine($"Error: Course Code {courseCode} not found.");
                return false;
            }

            Student s = Students[studentId];
            Course c = AvailableCourses[courseCode];

            if (s.AddCourse(c))
            {
                Console.WriteLine("Registration successful!");
                Console.WriteLine($"Total credits: {s.GetTotalCredits()}/{s.MaxCredits}.");
                return true;
            }
            return false;
        }

        public bool DropStudentFromCourse(string studentId, string courseCode)
        {
            if (!Students.ContainsKey(studentId))
            {
                Console.WriteLine($"Error: Student ID {studentId} not found.");
                return false;
            }

            Student s = Students[studentId];
            if (s.DropCourse(courseCode))
            {
                Console.WriteLine($"Successfully dropped {courseCode} from {studentId}.");
                return true;
            }
            else
            {
                Console.WriteLine($"Error: Student is not registered for {courseCode}.");
                return false;
            }
        }

        public void DisplayAllCourses()
        {
            Console.WriteLine("Available Courses");
            Console.WriteLine("Code\tName\tCredits\tEnrollment");
            foreach (var c in AvailableCourses.Values)
            {
                Console.WriteLine($"{c.CourseCode}\t{(c.CourseName.Length > 27 ? c.CourseName.Substring(0, 27) : c.CourseName)}\t{c.Credits}\t{c.GetEnrollmentInfo()}");
            }
        }

        public void DisplayStudentSchedule(string studentId)
        {
            if (Students.ContainsKey(studentId))
            {
                Students[studentId].DisplaySchedule();
            }
            else
            {
                Console.WriteLine($"Student ID {studentId} not found");
            }
        }

        public void DisplaySystemSummary()
        {
            double totalEnrollment = AvailableCourses.Values.Sum(c => c.GetCurrentEnrollment());
            double avgEnrollment = AvailableCourses.Count > 0 ? totalEnrollment / AvailableCourses.Count : 0;

            Console.WriteLine("\nSystem Summary:");
            Console.WriteLine($"- Total Students: {Students.Count}");
            Console.WriteLine($"- Total Courses: {AvailableCourses.Count}");
            Console.WriteLine($"- Average Enrollment: {avgEnrollment:F1}");
        }
    }
}
