using System;

namespace RobotHazardAnalysis
{
    public class RobotSafetyException : Exception
    {
        public RobotSafetyException(string message) : base(message) { }
    }

    public class RobotHazardAuditor
    {
        public double CalculateHazardRisk(double armPrecision, int workerDensity, string machineryState)
        {
            if (armPrecision < 0.0 || armPrecision > 1.0)
            {
                throw new RobotSafetyException("Error: Arm precision must be 0.0-1.0");
            }
            if (workerDensity < 1 || workerDensity > 20)
            {
                throw new RobotSafetyException("Error: Worker density must be 1-20");
            }
            double machineRiskFactor;
            switch (machineryState)
            {
                case "Worn":
                    machineRiskFactor = 1.3;
                    break;
                case "Faulty":
                    machineRiskFactor = 2.0;
                    break;
                case "Critical":
                    machineRiskFactor = 3.0;
                    break;
                default:
                    throw new RobotSafetyException("Error: Unsupported machinery state");
            }
            double hazardRisk = ((1.0 - armPrecision) * 15.0) + (workerDensity * machineRiskFactor);
            
            return hazardRisk;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter Arm Precision (0.0 - 1.0):");
                double armPrecision = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("Enter Worker Density (1 - 20):");
                int workerDensity = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter Machinery State (Worn/Faulty/Critical):");
                string machineryState = Console.ReadLine();

                RobotHazardAuditor auditor = new RobotHazardAuditor();
                double riskScore = auditor.CalculateHazardRisk(armPrecision, workerDensity, machineryState);

                Console.WriteLine($"Robot Hazard Risk Score: {riskScore}");
            }
            catch (RobotSafetyException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Invalid input format.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}