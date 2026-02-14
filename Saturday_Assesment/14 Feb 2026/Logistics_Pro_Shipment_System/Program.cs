using System;
public class Shipment
    {
        public string ShipmentCode { get; set; }
        public string TransportMode { get; set; }
        public double Weight { get; set; }
        public int StorageDays { get; set; }
    }

public class ShipmentDetails : Shipment
    {
        public bool ValidateShipmentCode()
        {
            if (string.IsNullOrEmpty(ShipmentCode) || ShipmentCode.Length != 7)
            {
                return false;
            }

            string prefix = ShipmentCode.Substring(0, 3);
            if (prefix != "GC#")
            {
                return false;
            }


            return true;
        }

        public double CalculateTotalCost()
        {
            double ratePerKg = 0.0;

            switch (TransportMode)
            {
                case "Sea":
                    ratePerKg = 15.00;
                    break;
                case "Air":
                    ratePerKg = 50.00;
                    break;
                case "Land":
                    ratePerKg = 25.00;
                    break;
                default:
                    Console.WriteLine("Invalid Transport Mode entered. Rate defaults to 0.");
                    break;
            }

            double parcelCost = (Weight * ratePerKg) + Math.Sqrt(StorageDays);

            return Math.Round(parcelCost, 2);
        }
    }
class Program
{
    static void Main(string[] args)
    {
        ShipmentDetails shipment = new ShipmentDetails();

        Console.Write("Enter Shipment Code: ");
        shipment.ShipmentCode = Console.ReadLine();

        if (!shipment.ValidateShipmentCode())
        {
            Console.WriteLine("Invalid shipment code");
            return;
        }
        Console.Write("Enter Transport Mode (Sea/Air/Land): ");
        shipment.TransportMode = Console.ReadLine();

            try
        {
            Console.Write("Enter Weight (kg): ");
            shipment.Weight = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter Storage Days: ");
            shipment.StorageDays = Convert.ToInt32(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid number format entered.");
            return;
        }

        double totalCost = shipment.CalculateTotalCost();

        Console.WriteLine($"The total shipping cost is {totalCost}");
        
    }
}