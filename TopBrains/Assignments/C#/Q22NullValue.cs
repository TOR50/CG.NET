using System;

public class Solution
{
    public static double? CalculateAverage(double?[] values)
    {
        double sum = 0;
        int count = 0;

        foreach (double? val in values)
        {
            if (val.HasValue)
            {
                sum += val.Value;
                count++;
            }
        }

        if (count == 0)
            return null;

        double average = sum / count;
        return Math.Round(average, 2, MidpointRounding.AwayFromZero);
    }

    public static void Main()
    {
        double?[] values = { 10.5, null, 20.0, null, 30.25 };

        double? result = CalculateAverage(values);

        if (result.HasValue)
        {
            Console.WriteLine("Average: " + result.Value);
        }
        else
        {
            Console.WriteLine("Average: null");
        }
    }
}
