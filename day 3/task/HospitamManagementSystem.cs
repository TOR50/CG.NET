class Patient
{
    public readonly int Pid ;
    public string Name;
    public int Age;
    private string History;

    public Patient(int id , string name , int age)
    {
        Pid = id;
        Name = name;
        Age = age;
    }

    public void SetHistory(string history) { History = history; }
    public string GetHistory() { return History; }
}

class Doctor
{
    public string Name;
    public string Specialization;
    public static int TotalDoctor = 0;

    public Doctor(string name, string spec)
    {
        Name = name;
        Specialization = spec;
        TotalDoctor++;
    }
}

class Appointment
{
    public void Schedule(string pname, string dname)
    {
        Console.WriteLine($"Basic Appointment : {pname} with {dname}");
    }

    public void Schedule(string pname, string dname, string date, string mode = "Offline")
    {
        Console.WriteLine($"Detailed Appointment : {pname} on {date} Mode : {mode}");
    }
}

class Diagnos
{
    public void Diag(in int age, ref string condition, out string risk, params int[] scores)
    {
        risk = (age > 60) ? "High" : "Normal";
        condition = "checked";
        Console.WriteLine($"Processing {scores.Length} test results");
    }
}

class Billing
{
    public double Fee { get; set; }
    public double Tests { get; set; }

    public double GetTotal() { return Fee + Tests; }
}