using System;

class Program
{
    static void Main()
    {
        // BankAccount acc = new BankAccount();  //object
        
        // acc.AccountNumber = 1001;
        // acc.Balance = 500.0;
        // Console.WriteLine($"Account {acc.AccountNumber} balance: {acc.Balance}");

        // Employee E1 = new Employee();

        // E1.Name="Tarun";
        // E1.Salary=123;
        // E1.Bonus=90;
        // E1.Display();

        Mathops m = new Mathops();

        int a = m.Add(10,20);
        int b = m.Add(10,10,20);
        double c = m.Add(2,2.554);
        Console.WriteLine(a);
        Console.WriteLine(b);
        Console.WriteLine(c);
        

        

    }
}
