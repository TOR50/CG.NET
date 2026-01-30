using System;
using System.Collections.Generic;

public abstract class Employee
{
    public abstract decimal GetPay();
}

public class HourlyEmployee : Employee
{
    private decimal rate;
    private decimal hours;

    public HourlyEmployee(decimal rate, decimal hours)
    {
        this.rate = rate;
        this.hours = hours;
    }

    public override decimal GetPay()
    {
        return rate * hours;
    }
}

public class SalariedEmployee : Employee
{
    private decimal monthlySalary;

    public SalariedEmployee(decimal monthlySalary)
    {
        this.monthlySalary = monthlySalary;
    }

    public override decimal GetPay()
    {
        return monthlySalary;
    }
}

public class CommissionEmployee : Employee
{
    private decimal commission;
    private decimal baseSalary;

    public CommissionEmployee(decimal commission, decimal baseSalary)
    {
        this.commission = commission;
        this.baseSalary = baseSalary;
    }

    public override decimal GetPay()
    {
        return baseSalary + commission;
    }
}

public class Solution
{
    public static decimal CalculateTotalPayroll(string[] employees)
    {
        decimal totalPay = 0;

        foreach (string emp in employees)
        {
            string[] parts = emp.Split(' ');
            Employee employee = null;

            if (parts[0] == "H")
            {
                decimal rate = decimal.Parse(parts[1]);
                decimal hours = decimal.Parse(parts[2]);
                employee = new HourlyEmployee(rate, hours);
            }
            else if (parts[0] == "S")
            {
                decimal salary = decimal.Parse(parts[1]);
                employee = new SalariedEmployee(salary);
            }
            else if (parts[0] == "C")
            {
                decimal commission = decimal.Parse(parts[1]);
                decimal baseSalary = decimal.Parse(parts[2]);
                employee = new CommissionEmployee(commission, baseSalary);
            }

            totalPay += employee.GetPay();
        }

        return Math.Round(totalPay, 2);
    }

    public static void Main()
    {
        string[] employees =
        {
            "H 100 10",
            "S 3000",
            "C 500 2500"
        };

        decimal total = CalculateTotalPayroll(employees);
        Console.WriteLine("Total Payroll: " + total);
    }
}
