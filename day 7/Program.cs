using System;
using System.Text;

public class Program
{
    public static void Main()
    {
        Console.Write("Provide a String Input : ");
        string st = Console.ReadLine();

        string result = CleanseAndInvert(st);

        Console.WriteLine(result);
    }

    public static string CleanseAndInvert(string input)
    {
        if (string.IsNullOrEmpty(input) || input.Length < 6)
        {
            return "Invalid Input";
        }

        foreach (char c in input)
        {
            if (!char.IsLetter(c))
            {
                return "Invalid Input";
            }
        }

        string lowered = input.ToLower();

        var filtered = new StringBuilder();
        foreach (char c in lowered)
        {
            if (((int)c % 2) != 0)
            {
                filtered.Append(c);
            }
        }

        char[] reversed = filtered.ToString().ToCharArray();
        Array.Reverse(reversed);

        for (int i = 0; i < reversed.Length; i++)
        {
            if (i % 2 == 0)
            {
                reversed[i] = char.ToUpper(reversed[i]);
            }
        }

        return new string(reversed);
    }
}