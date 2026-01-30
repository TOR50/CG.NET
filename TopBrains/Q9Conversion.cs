using System;

public class UnitConverter
{
    public static void Main(string[] args)
    {
        int feet = 5;
        double cm = ConvertFeetToCm(feet);
        
        Console.WriteLine($"{feet} feet is equal to {cm} cm.");
    }
    public static double ConvertFeetToCm(int feet)
    {
        if (feet < 0) return 0;
        double centimeters = feet * 30.48;
        return Math.Round(centimeters, 2, MidpointRounding.AwayFromZero);
    }
}
