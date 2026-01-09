using System;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

public class User
{
    public int Id{get;set;}
    public string Name{get;set;}

}
class Program
{
    static void Main()
    {
    //     FileInfo file = new FileInfo("sample.txt");
    //     if (!file.Exists)
    //     {
    //         using (StreamWriter writer = file.CreateText())
    //         {
    //             writer.WriteLine("hello fileinfo class");
    //         }
    //     }
    //     Console.WriteLine("File Name: " + file.Name);
    //     Console.WriteLine("File Size: " + file.Length + " bytes");
    //     Console.WriteLine("Created On: " + file.CreationTime);
    
    
    // Directory.CreateDirectory("Logs");
    // if (Directory.Exists("Logs"))
    // {
    //     Console.WriteLine("Logs director created sucessfully.");
    // }


    // DirectoryInfo  dir = new DirectoryInfo("Logs");
    //     if (!dir.Exists)
    //     {
    //         dir.Create();
    //     }
    //     Console.WriteLine("Directory Name : "+dir.Name);
    //     Console.WriteLine("Created On : "+dir.CreationTime);    
    //     Console.WriteLine("Full Path : "+dir.FullName);    


    // User user = new User{Id  = 1, Name = "roy"};
    // string json   = JsonSerializer.Serialize(user);
    // File.WriteAllText("user.json",json);
    // Console.WriteLine("User Serialized Sucessfully");


    // string json = File.ReadAllText("user.json");
    // User user = JsonSerializer.Deserialize<User>(json);
    // Console.WriteLine($"User Loader : {user.Id}, {user.Name}");


 
    User user = new User { Id = 1, Name = "Alice" };
    XmlSerializer serializer = new XmlSerializer (typeof(User));
    using (FileStream fs = new FileStream("user.xml",FileMode.Create))
    {
        serializer.Serialize(fs, user);
    }
    Console.WriteLine("XML Serialized");


    

    
    }
}