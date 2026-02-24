using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

class Program
{

    static void Main(string[] args)
    {
        string connectionString = "Server=ROY\\SQLEXPRESS;Database=AdoNet;Integrated Security=True;TrustServerCertificate=True;";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand("GetEmployeeCount", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter countParam = new SqlParameter("@TotalCount", SqlDbType.Int);
                countParam.Direction = ParameterDirection.Output;

                command.Parameters.Add(countParam);

                connection.Open();

                command.ExecuteNonQuery();
                int employeeCount = (int)command.Parameters["@TotalCount"].Value;

                Console.WriteLine($"Employee Count: {employeeCount}");
                
            }
        }
    }
}

//sql

CREATE TABLE Employees(
    EmpId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Branch VARCHAR(50)
);

CREATE PROCEDURE GetEmployeeCount
    @TotalCount INT OUTPUT
AS
BEGIN
    SELECT @TotalCount = COUNT(*) FROM Employees;
END
