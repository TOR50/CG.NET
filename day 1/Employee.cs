namespace HelloWorld
{
class Employee
{
    string Name;
    int id;
    string Department;
    float Salary;
    char Gender;
    
    public void AcceptDetails()
    {
        Console.WriteLine("Enter Employee Name:");
        Name = Console.ReadLine();
        Console.WriteLine("Enter Employee id");
        id = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter Employee Department:");
        Department = Console.ReadLine();
        Console.WriteLine("Enter Employee Salary:");
        Salary = Convert.ToSingle(Console.ReadLine());
        Console.WriteLine("Enter Employee Gender");
        Gender = Convert.ToChar(Console.ReadLine());
    }
    public void DisplayDetails()
    {
        Console.WriteLine($"Employee Name is: {Name}");
        Console.WriteLine($"Employee id is: {id}");
        Console.WriteLine($"Employee Department is: {Department}");
        Console.WriteLine($"Employee Salary is: {Salary}");
        Console.WriteLine($"Employee Gender is: {Gender}");
    }
}

}