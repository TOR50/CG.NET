using System;
using Microsoft.Data.SqlClient;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Server=ROY\\SQLEXPRESS;Database=AdoNet;Integrated Security=True;TrustServerCertificate=True;";

        Console.Write("Enter Sender Account ID: ");
        int senderId = int.Parse(Console.ReadLine());

        Console.Write("Enter Receiver Account ID: ");
        int receiverId = int.Parse(Console.ReadLine());

        Console.Write("Enter Amount: ");
        decimal amount = decimal.Parse(Console.ReadLine());

        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction();

        try
        {
            // Query 1: Deduct amount from Sender
            string query1 = "UPDATE Accounts SET Balance = Balance - @Amount WHERE AccountId = @AccountId";
            SqlCommand command1 = new SqlCommand(query1, connection, transaction);
            command1.Parameters.AddWithValue("@Amount", amount);
            command1.Parameters.AddWithValue("@AccountId", senderId);
            command1.ExecuteNonQuery();

            Console.WriteLine("Amount deducted from sender");

            // Query 2: Add amount to Receiver
            string query2 = "UPDATE Accounts SET Balance = Balance + @Amount WHERE AccountId = @AccountId";
            SqlCommand command2 = new SqlCommand(query2, connection, transaction);
            command2.Parameters.AddWithValue("@Amount", amount);
            command2.Parameters.AddWithValue("@AccountId", receiverId);
            command2.ExecuteNonQuery();

            Console.WriteLine("Amount added to receiver");

            transaction.Commit();
            Console.WriteLine("Transaction successful");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            transaction.Rollback();
            Console.WriteLine("Transaction rolled back");
        }

        connection.Close();
        Console.ReadLine();
    }
}


//sql
-- Create table
CREATE TABLE Accounts (
    AccountId INT PRIMARY KEY,
    AccountHolder NVARCHAR(100) NOT NULL,
    Balance DECIMAL(18, 2) NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE()
);
GO

-- Insert
INSERT INTO Accounts (AccountId, AccountHolder, Balance) VALUES
(1, 'rauhan', 5000.00),
(2, 'arjun', 3000.00),
(3, 'akash', 1500.00),
(4, 'karan', 10000.00);
GO

SELECT * FROM Accounts;
GO