using System;
public class Students
{
    public string Name{get;set;}
    public int Age{get;set;}
    public int Marks{get;set;}

    public Students(string name, int age, int marks)
    {
        Name = name;
        Age = age;
        Marks = marks;
    }
    public override string ToString()
    {
        return $"{Name} , {Age} , {Marks}";
    }
}

public class StudentCheck :  IComparer<Students>
{
    public int Compare(Students x, Students y)
    {
        int checkMark = y.Marks.CompareTo(x.Marks);
        if (checkMark != 0)
        {
            return checkMark;
        }
        return x.Age.CompareTo(y.Age);
    }
}


class Program
{
    static void Main()
    {
        List<Students> std = new List<Students>
        {
            new Students("Arjun", 22 , 85),
            new Students("vinay", 25 , 85),
            new Students("Tarun", 22 , 89 ),
            new Students("Shivansh", 21 , 100),
            new Students("Karan", 20 ,  99),
            new Students("Tanya", 19 ,  78)
        };

        std.Sort(new StudentCheck());
        std.ForEach(Console.WriteLine);
    }
}
