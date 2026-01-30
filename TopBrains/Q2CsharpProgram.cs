using System;
using System.Collections.Generic;
using System.Text;

public class MahirlAssignment
{
    public static void Main(string[] args)
    {
        string firstWord = Console.ReadLine();
        string secondWord = Console.ReadLine();

        if (string.IsNullOrEmpty(firstWord))
        {
            return;
        }

        HashSet<char> vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };
        HashSet<char> consonantsInSecond = new HashSet<char>();

        foreach (char c in secondWord.ToLower())
        {
            if (char.IsLetter(c) && !vowels.Contains(c))
            {
                consonantsInSecond.Add(c);
            }
        }

        StringBuilder step1Result = new StringBuilder();
        foreach (char c in firstWord)
        {
            char lowerC = char.ToLower(c);
            bool isVowel = vowels.Contains(lowerC);

            if (isVowel || !consonantsInSecond.Contains(lowerC))
            {
                step1Result.Append(c);
            }
        }

        if (step1Result.Length == 0)
        {
            Console.WriteLine("");
            return;
        }

        StringBuilder finalResult = new StringBuilder();
        finalResult.Append(step1Result[0]);

        for (int i = 1; i < step1Result.Length; i++)
        {
            if (char.ToLower(step1Result[i]) != char.ToLower(step1Result[i - 1]))
            {
                finalResult.Append(step1Result[i]);
            }
        }
        Console.WriteLine(finalResult.ToString());
    }
}