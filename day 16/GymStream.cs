using System;
namespace GymStream{

    public class InvalidTierException : Exception
    {
        public InvalidTierException(string message) : base(message){}
    }

    public class Membership
    {
        public string Tier{get;set;}
        public int DurationInMonths{get;set;}
        public double BasePricePerMonth{get;set;}

        public bool ValidateEnrollment()
        {
            if(Tier != "Elite" && Tier != "Premium" && Tier != "Basic")
            {
                throw new InvalidTierException("You are not Enrolled in any Tier");
            }
            if (DurationInMonths <= 0 )
            {
                throw new Exception("Duration is zero ");
            }

            return true;
        }

        public double CalculateTotalBill()
        {
            double total = (DurationInMonths * BasePricePerMonth);
            if(Tier == "Basic")
            {
                total = total - (total * 0.02);
            }
            else if (Tier == "Premium")
            {
                total = total - (total * 0.07);
            }
            else if(Tier == "Elite")
            {
                total = total - (total * 0.12);
            }
            return total;
        }

    }
class Program
{
    static void Main()
    {
        Membership member = new Membership();
         try
            {
                Console.WriteLine("--- GymStream Enrollment Portal ---");
                
                Console.WriteLine("Enter membership tier (Basic/Premium/Elite):");
                member.Tier = Console.ReadLine();

                Console.WriteLine("Enter duration in months:");
                member.DurationInMonths = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter base price per month:");
                member.BasePricePerMonth = Convert.ToDouble(Console.ReadLine());

                // Perform Validation
                if (member.ValidateEnrollment())
                {
                    Console.WriteLine("\nEnrollment Successful!");
                    double finalBill = member.CalculateTotalBill();
                    Console.WriteLine($"Total amount after discount: {finalBill:F2}");
                }
            }
            // Catch Block 1: Custom Exception for Tiers
            catch (InvalidTierException ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
            // Catch Block 2: General Exception for Duration or numeric errors
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to close...");
            Console.ReadKey();





    }
}
}