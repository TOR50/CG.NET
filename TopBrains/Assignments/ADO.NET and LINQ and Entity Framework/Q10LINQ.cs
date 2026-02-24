using System;
using System.Collections.Generic;
using System.Linq;

class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public decimal Salary { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        List<Employee> employees = new List<Employee>();
        employees.Add(new Employee { Id = 1, Name = "Arun", Department = "IT", Salary = 60000 });
        employees.Add(new Employee { Id = 2, Name = "Meera", Department = "HR", Salary = 45000 });
        employees.Add(new Employee { Id = 3, Name = "John", Department = "IT", Salary = 75000 });
        
        Console.WriteLine("All Employees");
        foreach (Employee emp in employees)
        {
            Console.WriteLine($"ID: {emp.Id}, Name: {emp.Name}, Department: {emp.Department}, Salary: {emp.Salary}");
        }

        List<Employee> filteredEmployees = new List<Employee>();
        foreach (Employee emp in employees)
        {
            if (emp.Salary > 50000)
            {
                filteredEmployees.Add(emp);
            }
        }

        Console.WriteLine("Filtered Employees (Salary > 50000)");
        foreach (Employee emp in filteredEmployees)
        {
            Console.WriteLine($"Name: {emp.Name}, Department: {emp.Department}, Salary: {emp.Salary}");
        }

        Dictionary<string, List<Employee>> groupedEmployees = new Dictionary<string, List<Employee>>();

        foreach (Employee emp in filteredEmployees)
        {
            if (!groupedEmployees.ContainsKey(emp.Department))
            {
                groupedEmployees[emp.Department] = new List<Employee>();
            }
            groupedEmployees[emp.Department].Add(emp);
        }

        Console.WriteLine("Grouped by Department");
        foreach (string department in groupedEmployees.Keys)
        {
            Console.Write($"{department} -> ");
            List<Employee> deptEmployees = groupedEmployees[department];
            for (int i = 0; i < deptEmployees.Count; i++)
            {
                Console.Write(deptEmployees[i].Name);
                if (i < deptEmployees.Count - 1)
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine();
        }

        Console.ReadLine();
    }
}