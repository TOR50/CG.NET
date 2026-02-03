using System;

public class Solution
{
    public static int SumOnlyIntegers(object[] values)
    {
        int sum = 0;

        foreach (object item in values)
        {
            if (item is int x)
            {
                sum += x;
            }
        }

        return sum;
    }

    public static void Main()
    {
        object[] values = { 10, "hello", 20, true, null, 5, 3.14 };

        int result = SumOnlyIntegers(values);
        Console.WriteLine("Sum of integers: " + result);
    }
}
