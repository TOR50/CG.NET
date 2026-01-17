using System;
public class Chocolate
{
    public string Flavour{get;set;}
    public int Quantity{get;set;}
    public int PricePerUnit{get;set;}
    public double TotalPrice{get;set;}
    public double DiscountedPrice{get;set;}

    public bool ValidateChocolateFlavour()
    {
        if (Flavour == "Dark" || Flavour == "Milk" || Flavour == "White")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

public class Program
{
    public Chocolate CalculateDiscountedPrice(Chocolate chocolate)
    {
        chocolate.TotalPrice = chocolate.Quantity * chocolate.PricePerUnit;
        double discountPercentage = 0;

        if (chocolate.Flavour == "Dark")
        {
            discountPercentage = 18;
        }
        else if (chocolate.Flavour == "Milk")
        {
            discountPercentage = 12;
        }
        else if (chocolate.Flavour == "White")
        {
            discountPercentage = 6;
        }

        chocolate.DiscountedPrice = chocolate.TotalPrice - (chocolate.TotalPrice * discountPercentage / 100);
        return chocolate;
    }

    public static void Main(string[] args)
    {
        Program program = new Program();
        Chocolate chocolate = new Chocolate();

        Console.WriteLine("Enter flavour (Milk/White/Dark)");
        chocolate.Flavour = Console.ReadLine();

        Console.WriteLine("Enter quantity");
        chocolate.Quantity = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter price per unit");
        chocolate.PricePerUnit = int.Parse(Console.ReadLine());

        if (chocolate.ValidateChocolateFlavour())
        {
            chocolate = program.CalculateDiscountedPrice(chocolate);

            Console.WriteLine($"Flavour : {chocolate.Flavour}");
            Console.WriteLine($"Quantity : {chocolate.Quantity}");
            Console.WriteLine($"Price Per Unit : {chocolate.PricePerUnit}");
            Console.WriteLine($"Total Price : {chocolate.TotalPrice}");
            Console.WriteLine($"Discounted Price : {chocolate.DiscountedPrice}");
        }
        else
        {
            Console.WriteLine("Invalid flavour");
        }
    }
}