using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace EmployeeOperationsTool
{
    class Program
    {
        static string connectionString = "Server=ROY\\SQLEXPRESS;Database=CompanyDB;Integrated Security=True;TrustServerCertificate=True;";

        static void Main(string[] args)
        {
            Console.WriteLine("Employee Operations Management System");
            Console.Write("Enter Department Name ( Sales, IT, HR): ");
            string dept = Console.ReadLine();

            Console.WriteLine("Part 1: Employees in Department");
            GetEmployeesByDepartment(dept);

            Console.WriteLine("Part 2: Department Strength");
            GetDepartmentEmployeeCount(dept);

            Console.WriteLine("Part 3: Employee Order Report");
            GetEmployeeOrders();

            Console.WriteLine("Part 4: Duplicate Employee Records");
            GetDuplicateEmployees();

            Console.ReadKey();
        }

        // Part 1
        static void GetEmployeesByDepartment(string department)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetEmployeesByDepartment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Department", department);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["EmpId"]}, Name: {reader["Name"]}, Phone: {reader["Phone"]}, Email: {reader["Email"]}");
                    }
                }
            }
        }

        // Part 2 (Incorporating Part 6 Fix)
        static void GetDepartmentEmployeeCount(string department)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetDepartmentEmployeeCount", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Department", department);

                
                SqlParameter outParam = new SqlParameter("@TotalEmployees", SqlDbType.Int);
                outParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outParam);

                con.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine($"Total employees in {department}: {outParam.Value}");
            }
        }

        // Part 3
        static void GetEmployeeOrders()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetEmployeeOrders", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Name: {reader["Name"]}, Dept: {reader["Department"]}, Order ID: {reader["OrderId"]}, Amount: ${reader["OrderAmount"]}, Date: {Convert.ToDateTime(reader["OrderDate"]).ToShortDateString()}");
                    }
                }
            }
        }

        // Part 4
        static void GetDuplicateEmployees()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetDuplicateEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    bool hasDuplicates = false;
                    while (reader.Read())
                    {
                        hasDuplicates = true;
                        Console.WriteLine($"Duplicate Found - ID: {reader["EmpId"]}, Name: {reader["Name"]}, Phone: {reader["Phone"]}, Email: {reader["Email"]}");
                    }
                    if (!hasDuplicates)
                    {
                        Console.WriteLine("No duplicate records found.");
                    }
                }
            }
        }
    }
}