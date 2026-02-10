using System;
using System.Collections.Generic;
using System.Linq;

namespace University_Course_Registration_System
{
     // =========================
    // Program (Menu-Driven)
    // =========================

    class Program
    {
        static void Main()
        {
            UniversitySystem system = new UniversitySystem();
            bool exit = false;

            Console.WriteLine("Welcome to University Course Registration System");

            while (!exit)
            {
                Console.WriteLine("\n1. Add Course");
                Console.WriteLine("2. Add Student");
                Console.WriteLine("3. Register Student for Course");
                Console.WriteLine("4. Drop Student from Course");
                Console.WriteLine("5. Display All Courses");
                Console.WriteLine("6. Display Student Schedule");
                Console.WriteLine("7. Display System Summary");
                Console.WriteLine("8. Exit");

                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            Console.Write("Enter Course Code: ");
                            string code = Console.ReadLine();
                            Console.Write("Enter Course Name: ");
                            string name = Console.ReadLine();
                            
                            Console.Write("Enter Credits (1-4): ");
                            int credits = int.Parse(Console.ReadLine());

                            Console.Write("Enter Max Capacity (default 50): ");
                            string capInput = Console.ReadLine();
                            int capacity = string.IsNullOrEmpty(capInput) ? 50 : int.Parse(capInput);

                            Console.Write("Enter Prerequisites (comma-separated, Enter for none): ");
                            string prereqInput = Console.ReadLine();
                            List<string> prereqs = new List<string>();
                            if (!string.IsNullOrEmpty(prereqInput))
                            {
                                prereqs = prereqInput.Split(',').Select(p => p.Trim()).ToList();
                            }

                            system.AddCourse(code, name, credits, capacity, prereqs);
                            break;

                        case "2":
                            Console.Write("Enter Student ID: ");
                            string id = Console.ReadLine();
                            Console.Write("Enter Name: ");
                            string sName = Console.ReadLine();
                            Console.Write("Enter Major: ");
                            string major = Console.ReadLine();

                            Console.Write("Enter Max Credits (default 18): ");
                            string maxCredInput = Console.ReadLine();
                            int maxCredits = string.IsNullOrEmpty(maxCredInput) ? 18 : int.Parse(maxCredInput);

                            Console.Write("Enter Completed Courses (comma-separated, Enter for none): ");
                            string compInput = Console.ReadLine();
                            List<string> completed = new List<string>();
                            if (!string.IsNullOrEmpty(compInput))
                            {
                                completed = compInput.Split(',').Select(c => c.Trim()).ToList();
                            }

                            system.AddStudent(id, sName, major, maxCredits, completed);
                            break;

                        case "3":
                            Console.Write("Enter Student ID: ");
                            string regId = Console.ReadLine();
                            Console.Write("Enter Course Code: ");
                            string regCode = Console.ReadLine();

                            system.RegisterStudentForCourse(regId, regCode);
                            break;

                        case "4":
                            Console.Write("Enter Student ID: ");
                            string dropId = Console.ReadLine();
                            Console.Write("Enter Course Code: ");
                            string dropCode = Console.ReadLine();

                            system.DropStudentFromCourse(dropId, dropCode);
                            break;

                        case "5":
                            system.DisplayAllCourses();
                            break;

                        case "6":
                            Console.Write("Enter Student ID: ");
                            string schedId = Console.ReadLine();
                            system.DisplayStudentSchedule(schedId);
                            break;

                        case "7":
                            system.DisplaySystemSummary();
                            break;

                        case "8":
                            exit = true;
                            Console.WriteLine("Exiting application.");
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Please enter 1-8.");
                            break;


                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}

