using System;
public class Program { 
    public static List<CreatorStats> EngagementBoard = new List<CreatorStats>();
public void RegisterCreator(CreatorStats record)
{
    EngagementBoard.Add(record);
}

public Dictionary<string, int> GetTopPostCounts(List<CreatorStats> records, double likeThreshold)
{
    Dictionary<string, int> result = new Dictionary<string, int>();
    foreach (CreatorStats item in records)
    {
        int count = 0;
        foreach (double likes in item.WeeklyLikes)
        {
            if (likes >= likeThreshold)
            {
                count++;
            }
        }
        if (count > 0)
        {
            result.Add(item.CreatorName, count);
        }
    }

    return result;
}

public double CalculateAverageLikes()
{
    double totalSum = 0;
    int totalWeeks = 0;
    foreach (CreatorStats item in EngagementBoard)
    {
        foreach (double likes in item.WeeklyLikes)
        {
            totalSum = totalSum + likes;
            totalWeeks = totalWeeks + 1;
        }
    }

    if (totalWeeks == 0)
    {
        return 0;
    }
    return totalSum / totalWeeks;
}

public static void Main(string[] args)
{
    Program StreamBuzz = new Program();
    bool loop = true;
    while (loop == true)
    {
        Console.WriteLine("1. Register Creator");
        Console.WriteLine("2. Show Top Posts");
        Console.WriteLine("3. Calculate Average Likes");
        Console.WriteLine("4. Exit");
        Console.WriteLine("Enter your choice :");

        int choice = Convert.ToInt32(Console.ReadLine());
        if (choice == 1)
        {
            Console.WriteLine("Enter Creator Name :");
            string name = Console.ReadLine();
            double[] weeklyData = new double[4];
            Console.WriteLine("Enter weekly likes (Week 1 to 4) : ");
            for (int i = 0; i < 4; i++)
            {
                weeklyData[i] = Convert.ToSingle(Console.ReadLine());
            }
            CreatorStats newCreator = new CreatorStats();
            newCreator.CreatorName = name;
            newCreator.WeeklyLikes = weeklyData;
            StreamBuzz.RegisterCreator(newCreator);
            Console.WriteLine("Creator registered successfully");
        }
        else if (choice == 2)
        {
            Console.WriteLine("Enter like threshold : ");
            double threshold = double.Parse(Console.ReadLine());
            Dictionary<string, int> tops = StreamBuzz.GetTopPostCounts(EngagementBoard, threshold);
            if (tops.Count == 0)
            {
                Console.WriteLine("No top performing posts this week");
            }
            else
            {
                foreach (var entry in tops)
                {
                    Console.WriteLine($"{entry.Key} - {entry.Value}");
                }
            }
            Console.WriteLine();
        }
        else if (choice == 3)
        {
            double avg = StreamBuzz.CalculateAverageLikes();
            Console.WriteLine($"Overall average weekly likes : {avg}");
            Console.WriteLine();
        }
        else if (choice == 4)
        {
            Console.WriteLine("Logging off - Keep Creating with StreamBuzz!");
            loop = false;
        }
    }
}
}