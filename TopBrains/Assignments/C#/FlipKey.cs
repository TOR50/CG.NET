using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter the word");
        string input = Console.ReadLine();

        Program p = new Program();
        string result = p.CleanseAndInvert(input);

        if (string.IsNullOrEmpty(result))
        {
            Console.WriteLine("Invalid Input");
        }
        else
        {
            Console.WriteLine($"The generated key is - {result}");
        }
    }

    public string CleanseAndInvert(string input)
    {
        if (string.IsNullOrEmpty(input) || input.Length < 6)
        {
            return string.Empty;
        }
        if (!Regex.IsMatch(input, "^[a-zA-Z]+$"))
        {
            return string.Empty;
        }
        string lowerInput = input.ToLower();

        StringBuilder filtered = new StringBuilder();
        foreach (char c in lowerInput)
        {
            if ((int)c % 2 != 0)
            {
                filtered.Append(c);
            }
        }

        char[] charArray = filtered.ToString().ToCharArray();
        Array.Reverse(charArray);
        
        StringBuilder finalKey = new StringBuilder();
        for (int i = 0; i < charArray.Length; i++)
        {
            if (i % 2 == 0)
            {
                finalKey.Append(char.ToUpper(charArray[i]));
            }
            else
            {
                finalKey.Append(charArray[i]);
            }
        }

        return finalKey.ToString();
    }
}