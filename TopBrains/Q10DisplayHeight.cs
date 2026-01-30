using System;

public class HeightClassifier
{
    public static void Main(string[] args)
    {
        Console.WriteLine(GetHeightCategory(145));
        Console.WriteLine(GetHeightCategory(165));
        Console.WriteLine(GetHeightCategory(185));
    }
    public static string GetHeightCategory(int heightCm)
    {
        if (heightCm < 150)
        {
            return "Short";
        }
        else if (heightCm >= 150 && heightCm < 180)
        {
            return "Average";
        }
        else 
        {
            return "Tall";
        }
    }
}
