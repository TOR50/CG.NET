using System;
using System.Linq;
class Program
{
    static void Main()
    {
        // Console.WriteLine("Creating objects...");
        
        // for(int i = 0 ; i < 5 ; i++)
        // {
        //     MyClass obj = new MyClass();
        // }
        // Console.WriteLine("Forcing garbage collection...");
        // GC.Collect();
        // GC.WaitForPendingFinalizers();
        // Console.WriteLine("Garbage collection complete");

        // var student = (ID: 101, Name: "amit");
        // var student2 = (ID : 102, Name: "ajay");
        // Console.WriteLine(student.GetType());
        // Console.WriteLine(student2.GetType());

        // var person = new { Name = "aryan", Age = 20};
        // Console.WriteLine(person.GetType());

        // Console.WriteLine(person.Age);
        // Console.WriteLine(student.ID);
        
        // static (int Sum , int Average , int diff) Calculate(int a , int b)
        // {
        //     return (a+b,(a+b)/2 , a-b );
        // }
        // var results = Calculate(10, 5);
        // Console.WriteLine($"Sum: {results.Sum}, Average: {results.Average}, Difference: {results.diff}");
        

        // static (bool IsValid, string Message) ValidateUser (string username)
        //     {
        //         if (string.IsNullOrEmpty(username))
        //             return (false, "Username is required");
                
        //         return (true, "Valid user");
        //     }
        // var response = ValidateUser("Admin");
        // Console.WriteLine(response. Message);


        // var person = (ID : 1001 , Name : "arun");
        // Console.WriteLine(person.ID);

        // var (id , name) = person;
        // Console.WriteLine(id);
        // Console.WriteLine(id.GetType());

        // var ( _ , userName) = person;

        // var s = new Student { Id = 1, Name = "Amit" };
        // s.GetType();
        // var (sid, sname) = s;

        // Console.WriteLine(sid);
        // Console.WriteLine(sname);

        // int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8 };
        // var evenNumbers = numbers. Where (n => n % 2 == 0); // LINQ
        // Console.WriteLine(evenNumbers.GetType());
        // Console.WriteLine("Even numbers are:");
        // foreach (var n in evenNumbers)
        // {
        //     Console.WriteLine(n);
        // }

        // numbers.Where(n => n >3).Select(n => n*2);
        // var result = from n in numbers where n > 3 select n * 2 ;

        // var students = new []
        // {
        //     new Student { Name = "Rohit", Marks = 62 },
        // };

        // var result = students.Select(s => new {
        //     s.Name,
        //     Grade = s.Marks > 60 ? "Pass" : "Fail"
        // });
        // Console.WriteLine("Result type: " + result.GetType());
        // foreach (var r in result)
        // {
        //     Console.WriteLine($"{r.Name}: {r.Grade}");
        // }



        // List<int> numbers = new List<int>{ 5 , 3 , 6 , 8 , 9 , 0 , 0 };
        // var ascending = numbers.OrderBy (n => n);
        // var descending = numbers.OrderByDescending (n => n);
        
        // Console.WriteLine("Ascending : ");
        // foreach (var n in ascending)
        // {
        //     Console.WriteLine(n);
        // }
        // Console.WriteLine("Descending : ");
        // foreach (var n in descending)
        // {
        //     Console.WriteLine(n);
        // }

        
        List<Employee> employees = new List<Employee>
        {
            new Employee { Name = "a", Salary = 10000 },
            new Employee { Name = "b", Salary = 20000 },
            new Employee { Name = "c", Salary = 30000 }
        };
        var sortedBySalary = employees.OrderBy (e => e.Salary);
        foreach (var e in sortedBySalary)
        {
            Console.WriteLine(e);
        }
    }
} 

class Employee
{
    public string Name { get; set; }
    public int Salary { get; set; }
}

// class Student
// {
//     public string Name;
//     public char Grade;
//     public int Marks;

// }

// class Student
// {
//     public int Id { get; set; }
//     public string Name { get; set; }

//     public void Deconstruct(out int id, out string name)
//     {
//         id = Id;
//         name = Name;
//     }
// }


// class MyClass
//     {
//         ~MyClass()
//         {
//             Console.WriteLine("Finalizer Called , object collected");
//         }
//     }