using System;

public class CircleCalculator
{
    public static void Main(string[] args)
    {
        double radius = 5.5;
        double result = GetCircleArea(radius);
        
        Console.WriteLine($"Radius: {radius}");
        Console.WriteLine($"Area (Rounded): {result}");
    }

    public static double GetCircleArea(double radius)
    {
        if (radius < 0) return 0;

        double area = Math.PI * Math.Pow(radius, 2);
        double roundedArea = Math.Round(area, 2, MidpointRounding.AwayFromZero);

        return roundedArea;
    }
}
