using System;
using System.Collections.Generic;

namespace StudentManagement
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string CourseName { get; set; }
        

        public Student(int id, string name, string course)
        {
            StudentId = id;
            StudentName = name;
            CourseName = course;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Student> studentList = new List<Student>();
            studentList.Add(new Student(1, "aaa", "Computer Science"));
            foreach (var student in studentList)
            {
                Console.WriteLine($"{student.StudentId} {student.StudentName} {student.CourseName}");
            }
        }
    }
}



