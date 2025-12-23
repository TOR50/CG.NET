using System;
class Program
{
    static void Main()
    {
        Console.WriteLine("Hospital Management System");
        Patient p = new Patient(222, "ramu", 63);
        p.SetHistory("no allergies");

        Doctor d = new Doctor("Dr abc", "dermatologist");
        Console.WriteLine($"Patient {p.Name} assigned to {d.Name}");

        Appointment appt = new Appointment();
        appt.Schedule(p.Name, d.Name);
        appt.Schedule(p.Name, d.Name, "dec 28", mode: "offline");

        Diagnos diag = new Diagnos();
        int age = p.Age;
        string status = "not available";
        diag.Diag(in age, ref status, out string riskLevel, 10, 20, 30);
        Console.WriteLine($"Diagnosis Results Risk is {riskLevel}");

        Billing myBill = new Billing { Fee = 500, Tests = 1000 };
        double total = myBill.GetTotal();

        Console.WriteLine($"Total Bill {total}");
        Console.WriteLine($"Insurance covers {total}");






    

    }
}