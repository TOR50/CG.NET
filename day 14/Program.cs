using System;
using System.IO;

class User
{
    public int ID;
    public string Name;
}
class Program
    {
    //     static void Main(string[] args)
    // {
        // string path = "data.txt";
        // File.WriteAllText(path , "File I/O Example in C#");
        // string content = File.ReadAllText("data.txt");
        // Console.WriteLine(content);
        // string path = "old_codes/log.txt"; 
        // using (StreamWriter writer = new StreamWriter(path))
        // {
        //     writer.WriteLine("Application Started");
        //     writer.WriteLine("Processing Data");
        //     writer.WriteLine("Application Ended");
        // }

        // using (StreamReader reader = new StreamReader("log.txt"))
        // {
        //     string line;
        //     while((line = reader.ReadLine()) != null)
        //     {
        //         Console.WriteLine(line);
        //     }
        // }
        
        // User user = new User{ID = 1 ,Name = "ajay"};
        // User user = new User{};
        // using (StreamWriter writer = new StreamWriter("user.txt"))
        // {
        //     writer.WriteLine(user.ID);
        //     writer.WriteLine(user.Name);
        // }
        // using (StreamReader reader = new StreamReader("user.txt"))
        // {
            // string line;
            // while((line = reader.ReadLine()) != null)
            // {
            //     Console.WriteLine(line);
            // }
    //         user.ID = int.Parse(reader.ReadLine());
    //         user.Name = reader.ReadLine();
    //     }
    //     Console.WriteLine($"user Loaded : {user.ID} : {user.Name}");
    // }

        static void Main()
        {
            User user = new User { ID = 2, Name = "Bob" };
            using (BinaryWriter writer = new BinaryWriter (File.Open("user.bin", FileMode.Create)))
                {
                    writer.Write(user.ID);
                    writer.Write(user.Name);
                }
            Console.WriteLine("Binary user data saved.");

            using (BinaryReader reader = new BinaryReader(File.Open("data.bin", FileMode.Open)))
        {
            Console.WriteLine(reader.ReadInt32());
            Console.WriteLine(reader.ReadString());
            
        }
        
        }
    }