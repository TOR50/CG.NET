using System;

public class MathUtils
{
    public static void Main(string[] args)
    {
        Console.WriteLine(GetLargest(10, 25, 15));  
        Console.WriteLine(GetLargest(-5, -2, -10)); 
        Console.WriteLine(GetLargest(100, 100, 50));
    }
    public static int GetLargest(int a, int b, int c)
    {
        int largest = a;

        if (b > largest)
        {
            largest = b;
        }

        if (c > largest)
        {
            largest = c;
        }

        return largest;
    }
}
