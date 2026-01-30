using System;
using System.Collections.Generic;

public interface IArea
{
    double CalculateArea();
}

public abstract class Shape : IArea
{
    public abstract double CalculateArea();
}

public class Circle : Shape
{
    private double radius;

    public Circle(double radius)
    {
        this.radius = radius;
    }

    public override double CalculateArea()
    {
        return Math.PI * radius * radius;
    }
}

public class Rectangle : Shape
{
    private double width;
    private double height;

    public Rectangle(double width, double height)
    {
        this.width = width;
        this.height = height;
    }

    public override double CalculateArea()
    {
        return width * height;
    }
}

public class Triangle : Shape
{
    private double b;
    private double h;

    public Triangle(double b, double h)
    {
        this.b = b;
        this.h = h;
    }

    public override double CalculateArea()
    {
        return 0.5 * b * h;
    }
}

public class Solution
{
    public static double CalculateTotalArea(string[] shapes)
    {
        double totalArea = 0;

        foreach (string shape in shapes)
        {
            string[] parts = shape.Split(' ');
            Shape obj = null;

            if (parts[0] == "C")
            {
                double r = double.Parse(parts[1]);
                obj = new Circle(r);
            }
            else if (parts[0] == "R")
            {
                double w = double.Parse(parts[1]);
                double h = double.Parse(parts[2]);
                obj = new Rectangle(w, h);
            }
            else if (parts[0] == "T")
            {
                double b = double.Parse(parts[1]);
                double h = double.Parse(parts[2]);
                obj = new Triangle(b, h);
            }

            totalArea += obj.CalculateArea();
        }

        return Math.Round(totalArea, 2, MidpointRounding.AwayFromZero);
    }

    public static void Main()
    {
        string[] shapes =
        {
            "C 5",
            "R 4 6",
            "T 3 8"
        };

        double result = CalculateTotalArea(shapes);
        Console.WriteLine("Total Area: " + result);
    }
}
