class BankAccount()
{
    public int AccountNumber;
    public double Balance;
    public void DisplayAccountInfo()
    {
        Console.WriteLine($"Account Number: {AccountNumber}");
        Console.WriteLine($"Balance: {Balance}");
    }
    
}
        

class Employee()
{
    public string Name;
    public double Salary;
    public double Bonus;

    public void Display()
    {
        Console.WriteLine($"Employee Name: {Name}");
        Console.WriteLine($"Salary: {Salary}");
        Console.WriteLine($"Bonus: {Bonus}");
    }

}
