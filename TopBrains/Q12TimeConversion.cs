using System;

public class TimeConverter
{
    public static void Main(string[] args)
    {
        Console.WriteLine(FormatTime(125));
        Console.WriteLine(FormatTime(60));
        Console.WriteLine(FormatTime(9));
    }
    public static string FormatTime(int totalSeconds)
    {
        int minutes = totalSeconds / 60;

        int seconds = totalSeconds % 60;

        return $"{minutes}:{seconds:D2}";
    }
}
