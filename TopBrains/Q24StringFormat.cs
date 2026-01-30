using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public record Student(string Name, int Score);

public class Solution
{
    public static string BuildStudentJson(string[] items, int minScore)
    {
        List<Student> students = new List<Student>();

        foreach (string item in items)
        {
            string[] parts = item.Split(':');
            if (parts.Length == 2 && int.TryParse(parts[1], out int score))
            {
                students.Add(new Student(parts[0], score));
            }
        }

        var filteredSorted = students
            .Where(s => s.Score >= minScore)
            .OrderByDescending(s => s.Score)
            .ThenBy(s => s.Name)
            .ToList();

        return JsonSerializer.Serialize(filteredSorted);
    }

    public static void Main()
    {
        string[] items =
        {
            "Rauhan:85",
            "kunal:92",
            "Anand:85",
            "Taara:70",
            "Akash:95"
        };

        int minScore = 85;

        string jsonResult = BuildStudentJson(items, minScore);
        Console.WriteLine(jsonResult);
    }
}
