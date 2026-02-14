using System;

namespace HealthSyncBilling
{
    public abstract class Consultant
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public Consultant(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public abstract double CalculateGrossPayout();

        public virtual double CalculateTDS(double grossEarnings)
        {
            return (grossEarnings <= 5000) ? grossEarnings * 0.05 : grossEarnings * 0.15;
        }

        public virtual string GetTaxRateLabel(double grossEarnings)
        {
             return (grossEarnings <= 5000) ? "5%" : "15%";
        }

        public static bool ValidateConsultantId(string id)
        {
            if (string.IsNullOrEmpty(id) || id.Length != 6) return false;
            if (!id.StartsWith("DR")) return false;
            string numberPart = id.Substring(2);
            return int.TryParse(numberPart, out _);
        }

        public void PrintPayslip()
        {
            double gross = CalculateGrossPayout();
            double tax = CalculateTDS(gross);
            double net = gross - tax;
            string taxLabel = GetTaxRateLabel(gross);

            Console.WriteLine($"Output: Gross: {gross} | TDS Applied: {taxLabel} | Net Payout: {net}");
        }
    }
    public class InHouseConsultant : Consultant
    {
        public double MonthlyStipend { get; set; }
        private const double TravelAllowance = 2000;
        private const double PerformanceBonus = 1000;

        public InHouseConsultant(string id, string name, double stipend) : base(id, name)
        {
            MonthlyStipend = stipend;
        }

        public override double CalculateGrossPayout()
        {
            return MonthlyStipend + TravelAllowance + PerformanceBonus;
        }
    }

    public class VisitingConsultant : Consultant
    {
        public int ConsultationsCount { get; set; }
        public double RatePerVisit { get; set; }

        public VisitingConsultant(string id, string name, int count, double rate) : base(id, name)
        {
            ConsultationsCount = count;
            RatePerVisit = rate;
        }

        public override double CalculateGrossPayout()
        {
            return ConsultationsCount * RatePerVisit;
        }

        public override double CalculateTDS(double grossEarnings)
        {
            return grossEarnings * 0.10;
        }

        public override string GetTaxRateLabel(double grossEarnings)
        {
            return "10% (Flat)";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("HealthSync Advanced Billing System");

            while (true)
            {
                Console.WriteLine("Select Consultant Type");
                Console.WriteLine("1. In-House Consultant");
                Console.WriteLine("2. Visiting Consultant");
                Console.WriteLine("3. Exit");
                Console.Write("Enter Choice: ");
                string choice = Console.ReadLine();

                if (choice == "3") break;

                if (choice != "1" && choice != "2")
                {
                    Console.WriteLine("Invalid choice.");
                    continue;
                }

                Console.Write("Enter Consultant ID (DR0000) : ");
                string id = Console.ReadLine();
                if (!Consultant.ValidateConsultantId(id))
                {
                    Console.WriteLine("Output: Invalid doctor id");
                    continue;
                }

                Console.Write("Enter Consultant Name: ");
                string name = Console.ReadLine();

                Consultant doctor = null;

                try
                {
                    if (choice == "1")
                    {
                        Console.Write("Enter Monthly Stipend: ");
                        double stipend = double.Parse(Console.ReadLine());
                        doctor = new InHouseConsultant(id, name, stipend);
                    }
                    else if (choice == "2")
                    {
                        Console.Write("Enter Number of Consultations: ");
                        int count = int.Parse(Console.ReadLine());
                        Console.Write("Enter Rate Per Visit: ");
                        double rate = double.Parse(Console.ReadLine());
                        doctor = new VisitingConsultant(id, name, count, rate);
                    }
                    Console.WriteLine($"ID: {doctor.Id} | Name: {doctor.Name}");
                    doctor.PrintPayslip();
                }
                catch (FormatException)
                {
                    Console.WriteLine("Enter valid numbers for Stipend/Rate/Count.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"error {ex.Message}");
                }
            }
        }
    }
}