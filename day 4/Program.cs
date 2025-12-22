using System;
class Program
{
    static void Main()
    {
        // Student st = new Student();

        // st.Name="ajay";
        // st.Age=20;
        // st.Marks=95;
        // st.Pass="jhhjjjhjjjh";

        // Console.WriteLine(st.Name);
        // Console.WriteLine(st.Age);
        // Console.WriteLine(st.Marks);
        // Library l = new Library();
        // l[101] = "C# Basics";
        // Console.WriteLine(l[101]);
        // Console.WriteLine(l["C# Basics"]);
        // Console.WriteLine(l[999]);

        Auth auth = new Auth();
    auth.Message();

    Lic lifePolicy = new Lic("101", "Amit", 5000);
    HealthInsurance healthPolicy = new HealthInsurance("102", "Neha", 8000);

    PolicyDirectory directory = new PolicyDirectory();
    directory.AddPolicy(lifePolicy);
    directory.AddPolicy(healthPolicy);

    Console.WriteLine(directory[0].Name);

    Console.WriteLine(directory["Neha"].Policynum);

    lifePolicy.CalcPremium();
    healthPolicy.CalcPremium();

    lifePolicy.ShowPolicy();
    InsurancePolicy baseRef = lifePolicy;
    baseRef.GenericPolicy();
        

    }
}