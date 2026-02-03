using System;
using System.Collections.Generic;

public class SalaryCalculator
{
    public static void Main(string[] args)
    {
        Dictionary<int, int> employeeSalaries = new Dictionary<int, int>()
        {
            { 1, 20000 },
            { 4, 40000 },
            { 5, 15000 }
        };

        int[] searchIds = { 1, 4, 5 };

        int totalSalary = 0;

        foreach (int id in searchIds)
        {
            if (employeeSalaries.ContainsKey(id))
            {
                totalSalary += employeeSalaries[id];
            }
        }
        Console.WriteLine(totalSalary);
    }
}
