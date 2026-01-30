using System;

public class PositiveSumCalculator
{
    public static void Main(string[] args)
    {
        int[] nums = { 10, -5, 20, 0, 30 };
        int result = SumPositivesUntilZero(nums);
        Console.WriteLine($"Total Sum: {result}");
    }

    public static int SumPositivesUntilZero(int[] nums)
    {
        long currentSum = 0;

        foreach (int n in nums)
        {
            if (n == 0)
            {
                break;
            }
            if (n < 0)
            {
                continue;
            }

            currentSum += n;
        }

        return (int)currentSum;
    }
}
