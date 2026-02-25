using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

class Customer
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
}

class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
}

class Order
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}

class OrderItem
{
    public int OrderItemId { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

class AppDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=ROY\\SQLEXPRESS;Database=AdoNet;Integrated Security=True;TrustServerCertificate=True;");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter Customer ID: ");
        int customerId = int.Parse(Console.ReadLine());

        Console.Write("How many products to order? ");
        int productCount = int.Parse(Console.ReadLine());

        List<OrderItem> items = new List<OrderItem>();

        for (int i = 0; i < productCount; i++)
        {
            Console.Write($"\nProduct {i + 1} - Enter Product ID: ");
            int productId = int.Parse(Console.ReadLine());

            Console.Write($"Product {i + 1} - Enter Quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            OrderItem item = new OrderItem();
            item.ProductId = productId;
            item.Quantity = quantity;
            items.Add(item);
        }

        AppDbContext db = new AppDbContext();

        Order order = new Order();
        order.CustomerId = customerId;
        order.OrderDate = DateTime.Now;
        order.OrderItems = items;

        db.Orders.Add(order);
        db.SaveChanges();

        Console.WriteLine($"Order created successfully! Order ID: {order.OrderId}");

        Console.WriteLine("Order Details");
        Console.WriteLine($"Order ID: {order.OrderId}");
        Console.WriteLine($"Customer ID: {order.CustomerId}");
        Console.WriteLine($"Order Date: {order.OrderDate}");
        Console.WriteLine("Order Items:");
        foreach (OrderItem item in order.OrderItems)
        {
            Console.WriteLine($"  Product ID: {item.ProductId}, Quantity: {item.Quantity}");
        }

        Console.ReadLine();
    }
}

USE AdoNet;
GO

-- Create Customers table
CREATE TABLE Customers (
    CustomerId INT PRIMARY KEY,
    CustomerName NVARCHAR(100) NOT NULL
);
GO

-- Create Products table
CREATE TABLE Products (
    ProductId INT PRIMARY KEY,
    ProductName NVARCHAR(100) NOT NULL,
    Price DECIMAL(18, 2) NOT NULL
);
GO

-- Create Orders table
CREATE TABLE Orders (
    OrderId INT PRIMARY KEY IDENTITY(1,1),
    CustomerId INT NOT NULL,
    OrderDate DATETIME NOT NULL,
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
);
GO

-- Create OrderItems table
CREATE TABLE OrderItems (
    OrderItemId INT PRIMARY KEY IDENTITY(1,1),
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);
