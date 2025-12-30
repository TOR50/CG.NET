using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        // bool res = Regex.IsMatch("abc123",@"\d");
        // bool res = Regex.IsMatch("123_123",@"\d");
        // Console.WriteLine(res);

        // Match m = Regex.Match("10 20 30 ! @ _ abc ABC file.txt C:\abc\file.txt", @"\?+");
        // Console.WriteLine($"value {m.Value} : index {m.Index}");
        // Console.WriteLine("----------------------------");
        // MatchCollection matches = Regex.Matches("10 20 30 ! @ _ abc ABC ? file.txt C:\abc\file.txt ?", @"\?+"); // d, D, w, W, s, S, n, \.(), ^ab, $C, 
        // foreach (Match match in matches)
        // {
        //     Console.WriteLine($"value {match.Value} : index {match.Index}");
        // }

        // string sentence = "Amount=5000";
        // string pattern = @"Amount=(?<value>\d+)";
        // Match m = Regex.Match(sentence, pattern);
        // if (m.Success)
        // {
        //     Console.WriteLine("Extracted Value: " + m.Groups["value"].Value);
        // }



        // string pattern = @"(?<year>\d{4})[-/](?<month>\d{2})[-/](?<date>\d{2})";
        // string input = "23-02-1992";
        //     // "1992-02-23, 1990-01-01";
        //     // "1992/02/23, 1990-01-01";
        //     // "1992-02-23, 1990-01-01, 2025";

        // Console.WriteLine($"Input {input}");
        // Match m = Regex.Match(input, pattern);
        // if (m.Success)
        // {
        //     Console.WriteLine($"year = {m.Groups["year"].Value}, month = {m.Groups["month"].Value}");
        // }
        // else
        // {
        //     Console.WriteLine("no match");
        // }

        // MatchCollection m2 = Regex.Matches(input, pattern);
        // if (m2.Count > 1)
        // {
        //     Console.WriteLine($"matches {m2.Count} : ");
        //     foreach (Match x in m2)
        //     {
        //         Console.WriteLine($" Found {x.Groups["year"].Value} month {x.Groups["month"].Value}");
        //     }
        // }
        bool loop = true;
        while(loop){
        Console.WriteLine("");
        Console.Write("Enter Email : ");
        string input = Console.ReadLine();
        if (input == "exit")
            {
                loop = false;
                return;
            }
        Console.WriteLine("");
        string pattern = @"\b[\w.-]+@[\w.-]+\.\w{2,}\b";
        if (Regex.IsMatch(input, pattern))
        {
            Console.WriteLine("Valid Email found");
        }
        else
        {
            Console.WriteLine("Invalid Email");
        }
        }


    }
}