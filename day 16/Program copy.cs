using System;
using System.Collections.Generic;
using System.Linq;

// class User
//     {
//         public string Name {get; set;}
//         public int Age {get; set;}
//     }


// class Student
// {
//     public string Name { get; set; }
//     public string Grade { get; set; }
//     public int Marks { get; set; }
// }

// class Employee
//     {
//         public string Name{get;set;}
//         public int Salary{get;set;}
//     }
class Program
{
    static void Main(string[] args)
    {

        // int total = 0;
        // for(int i=1; i<=5; i++)
        // {
        //     // Console.WriteLine("The value of i is " + i);
        //     // Console.WriteLine("Before loop total value " + total);
        //     total += i;
        //     // Console.WriteLine("After the loop total value " + total);

        // }



        // List<User> users = new List<User>();

        // users.Add(new User{Name = "Aanand", Age = 20});
        // users.Add(new User{Name = "Ayush", Age = 21});
        // users.Add(new User{Name = "Raushan", Age = 21});
        // users.Add(new User{Name = "Rohan", Age = 63});
        // users.Add(new User{Name = "Mohit", Age = 52});

        // foreach(var user in users)
        // {
        //     Console.WriteLine($"User Name: {user.Name}, User Age: {user.Age}");
        // }

        // Queue<int> queue = new Queue<int>();
        // queue.Enqueue(45);
        // queue.Enqueue(55);
        // queue.Enqueue(65);
        // queue.Enqueue(75);
        // queue.Enqueue(25);

        // while(queue.Count > 0)
        // {
        //     Console.Write(queue.Dequeue() + " ");
        // }


        
// Create three objects and assign them value
        
        // List<Student> students = new List<Student>
        // {
        //     new Student { Name = "rauhan", Marks = 95 },
        //     new Student { Name = "aanand", Marks = 95 },
        //     new Student { Name = "arjun", Marks = 50 }
        // };

        // var result = students.Select(s => new
        // {
        //     s.Name,
        //     Grades = s.Marks > 60 ? "Pass" : "Fail"
        // });

        // foreach (var i in result)
        // {
        //     Console.WriteLine($"{i.Name}: {i.Grades}");
        // }
        // Console.WriteLine($"\nResult type: {result.GetType().Name}");


    
    // List<Employee> employees = new List<Employee>
    // {
    //     new Employee { Name = "Amit", Salary = 50000 },
    //     new Employee { Name = "Ravi", Salary = 70000 },
    //     new Employee { Name = "Neha", Salary = 60000 }
    // };
    // var sortedBySalary = employees.OrderBy (e => e.Salary);
    // var sortedByName = employees.OrderBy (e => e.Name);

    // Console.WriteLine(string.Join(", ", sortedBySalary.Select(e => $"{e.Name}: {e.Salary}")));


    // foreach (var i in sortedBySalary)   
    // {
    //     Console.WriteLine($"{i.Name}: {i.Salary}");
    // }

    List<int> numbers = new List<int> {10,20,30,2,0,-85};
    int first = numbers.First();
    Console.WriteLine(first);

    int last = numbers.Last();
    Console.WriteLine(last);


    }
}


