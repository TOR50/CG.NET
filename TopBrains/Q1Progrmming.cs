using System;

public class LuckyDraw
{
    public static void Main(string[] args)
    {
        string[] input = Console.ReadLine().Split(' ');
        if (input.Length < 2) return;

        int m = int.Parse(input[0]);
        int n = int.Parse(input[1]);

        int luckyCount = 0;

        for (int i = m; i <= n; i++)
        {
            if (IsLucky(i))
            {
                luckyCount++;
            }
        }

        Console.WriteLine(luckyCount);
    }

    public static bool IsLucky(int x)
    {
        if (IsPrime(x)) return false;

        long sumX = SumDigits(x);
        long sumXSquared = SumDigits((long)x * x);

        return sumXSquared == (sumX * sumX);
    }

    public static long SumDigits(long num)
    {
        long sum = 0;
        num = Math.Abs(num);
        while (num > 0)
        {
            sum += num % 10;
            num /= 10;
        }
        return sum;
    }

    public static bool IsPrime(int number)
    {
        if (number <= 1) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;

        var boundary = (int)Math.Floor(Math.Sqrt(number));

        for (int i = 3; i <= boundary; i += 2)
        {
            if (number % i == 0) return false;
        }

        return true;
    }
}