using System;
using System.Collections.Generic;

public static class StringExtensions
{
    public static string[] DistinctById(this string[] items)
    {
        if (items == null || items.Length == 0)
            return Array.Empty<string>();

        HashSet<string> seenIds = new HashSet<string>();
        List<string> result = new List<string>();

        foreach (string item in items)
        {
            string[] parts = item.Split(':');
            if (parts.Length != 2)
                continue;

            string id = parts[0];
            string name = parts[1];

            if (!seenIds.Contains(id))
            {
                seenIds.Add(id);
                result.Add(name);
            }
        }

        return result.ToArray();
    }
}

public class Solution
{
    public static void Main()
    {
        string[] items =
        {
            "1:Rauhan",
            "2:kunal",
            "1:Anand",
            "3:Taara",
            "2:Akash"
        };

        string[] distinctNames = items.DistinctById();

        foreach (string name in distinctNames)
        {
            Console.WriteLine(name);
        }
    }
}
