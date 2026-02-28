using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Server=ROY\\SQLEXPRESS;Database=AdoNet;Integrated Security=True;TrustServerCertificate=True;";

        ProductDataAccess dataAccess = new ProductDataAccess();

        List<Product> products = dataAccess.GetAllProducts(connectionString);

            Console.WriteLine("Product List");
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.ProductId} Name: {product.Name} Price: {product.Price} Stock: {product.StockQuantity}");
            }

        Console.ReadLine();
    }
}

//product data acess 
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

class ProductDataAccess
{
    public List<Product> GetAllProducts(string connectionString)
    {
        List<Product> products = new List<Product>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT ProductId, Name, Price, StockQuantity FROM Products";

            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        ProductId = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Price = reader.GetDecimal(2),
                        StockQuantity = reader.GetInt32(3)
                    });
                }
            }
        }

        return products;
    }
}

//class product
using System;

class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}

// sql query

CREATE TABLE Products (
    ProductId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Price DECIMAL(18, 2) NOT NULL,
    StockQuantity INT NOT NULL
);

-- Insert sample data
INSERT INTO Products (Name, Price, StockQuantity) VALUES
('Laptop', 5999.99, 15),
('Mouse', 299.99, 50),
('Keyboard', 579.99, 30),
('Monitor', 2999.99, 20);