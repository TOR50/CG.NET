using System;
using System.IO;

public class LogFilter
{
    public static void Main(string[] args)
    {
        string inputPath = "log.txt";
        string outputPath = "error.txt";

        try
        {
            if (!File.Exists(inputPath))
            {
                Console.WriteLine("Error: Source log file not found.");
                return;
            }
            using (StreamReader reader = new StreamReader(inputPath))
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("ERROR", StringComparison.OrdinalIgnoreCase))
                    {
                        writer.WriteLine(line);
                    }
                }
            }

            Console.WriteLine("Filtering complete. Errors saved to error.txt.");
        }
        catch (IOException e)
        {
            Console.WriteLine($"An error occurred while processing files: {e.Message}");
        }
    }
}
