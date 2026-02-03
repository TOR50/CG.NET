using System;
using System.Collections.Generic;

public class Bike
{
    public string Model { get; set; }
    public string Brand { get; set; }
    public int PricePerDay { get; set; }
}

public class BikeUtility
{
    public void AddBikeDetails(string model, string brand, int pricePerDay)
    {
        Bike b = new Bike();
        b.Model = model;
        b.Brand = brand;
        b.PricePerDay = pricePerDay;

        int id = Program.bikeDetails.Count + 1;
        Program.bikeDetails.Add(id, b);

        Console.WriteLine("Bike details added successfully");
    }

    public SortedDictionary<string, List<Bike>> GroupBikesByBrand()
    {
        SortedDictionary<string, List<Bike>> result = new SortedDictionary<string, List<Bike>>();

        foreach (var item in Program.bikeDetails)
        {
            Bike b = item.Value;
            if (result.ContainsKey(b.Brand) == false)
            {
                result.Add(b.Brand, new List<Bike>());
            }
            result[b.Brand].Add(b);
        }
        return result;
    }
}

class Program
{
    public static SortedDictionary<int, Bike> bikeDetails = new SortedDictionary<int, Bike>();

    static void Main()
    {
        BikeUtility util = new BikeUtility();

        while (true)
        {
            Console.WriteLine("1. Add Bike Details");
            Console.WriteLine("2. Group Bikes By Brand");
            Console.WriteLine("3. Exit");
            Console.WriteLine("Enter your choice");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.WriteLine("Enter the model");
                string m = Console.ReadLine();
                Console.WriteLine("Enter the brand");
                string br = Console.ReadLine();
                Console.WriteLine("Enter the price per day");
                int p = int.Parse(Console.ReadLine());

                util.AddBikeDetails(m, br, p);
            }
            else if (choice == "2")
            {
                var groups = util.GroupBikesByBrand();

                foreach (var group in groups)
                {
                    Console.WriteLine(group.Key);
                    foreach (var bike in group.Value)
                    {
                        Console.WriteLine(bike.Model);
                    }
                }
            }
            else if (choice == "3")
            {
                break;
            }
        }
    }
}
