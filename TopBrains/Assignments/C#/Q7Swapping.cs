using System;

public class RefSwapper
{
    public static void Main()
    {
        int a = 10, b = 20;
        Console.WriteLine($"Before Swap: a = {a}, b = {b}");

        SwapRef(ref a, ref b);

        Console.WriteLine($"After Swap:  a = {a}, b = {b}");
    }

    public static void SwapRef(ref int x, ref int y)
    {
        x = x + y; 
        y = x - y; 
        x = x - y; 
    }
}
