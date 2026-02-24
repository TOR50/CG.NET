using System;
using System.Data;
using Microsoft.Data.SqlClient;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Server=ROY\\SQLEXPRESS;Database=AdoNet;Integrated Security=True;TrustServerCertificate=True;";

        SqlConnection connection = new SqlConnection(connectionString);
        string query = "SELECT * FROM Products";
        SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

        DataSet dataset = new DataSet();
        adapter.Fill(dataset, "Products");

        Console.WriteLine("Dataset filled with data from database\n");

        DataTable table = dataset.Tables["Products"];
        foreach (DataRow row in table.Rows)
        {
            Console.WriteLine($"ID: {row["ProductId"]}, Name: {row["ProductName"]}, Price: {row["Price"]}, Stock: {row["Stock"]}");
        }

        Console.Write("Enter Product ID to update: ");
        int productId = int.Parse(Console.ReadLine());

        Console.Write("Enter new Price: ");
        decimal newPrice = decimal.Parse(Console.ReadLine());

        Console.Write("Enter new Stock: ");
        int newStock = int.Parse(Console.ReadLine());

        foreach (DataRow row in table.Rows)
        {
            if ((int)row["ProductId"] == productId)
            {
                row["Price"] = newPrice;
                row["Stock"] = newStock;
                Console.WriteLine("Row modified in dataset");
                break;
            }
        }

        Console.WriteLine("Modified Data (in Dataset)");
        foreach (DataRow row in table.Rows)
        {
            Console.WriteLine($"ID: {row["ProductId"]}, Name: {row["ProductName"]}, Price: {row["Price"]}, Stock: {row["Stock"]}");
        }

        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
        int rowsUpdated = adapter.Update(dataset, "Products");

        Console.WriteLine($"Number of rows updated in database: {rowsUpdated}");
        Console.WriteLine("Changes saved to database successfully");

        // Verify changes in database
        DataSet verifyDataset = new DataSet();
        adapter.Fill(verifyDataset, "Products");
        DataTable verifyTable = verifyDataset.Tables["Products"];
        foreach (DataRow row in verifyTable.Rows)
        {
            Console.WriteLine($"ID: {row["ProductId"]}, Name: {row["ProductName"]}, Price: {row["Price"]}, Stock: {row["Stock"]}");
        }

        connection.Close();
        Console.ReadLine();
    }
}


//sql

CREATE TABLE Products (
    ProductId INT PRIMARY KEY,
    ProductName NVARCHAR(100) NOT NULL,
    Price DECIMAL(18, 2) NOT NULL,
    Stock INT NOT NULL
);

INSERT INTO Products (ProductId, ProductName, Price, Stock) VALUES
(1, 'Laptop', 45000.00, 10),
(2, 'Mouse', 500.00, 50),
(3, 'Keyboard', 1200.00, 30),
(4, 'Monitor', 15000.00, 15),
(5, 'Headphone', 2000.00, 25);
