using System;
using System.Data.SqlClient;
namespace StudentDatabaseApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int studentId = 1;
            string studentName = "Rauhan Kumar Roy";
            decimal studentMarks = 99.5m;

            InsertStudent(studentId, studentName, studentMarks);
            
            Console.ReadLine();
        }

        static void InsertStudent(int id, string name, decimal marks)
        {
            string connectionString = "Server=YOUR_SERVER_NAME;Database=YOUR_DATABASE_NAME;Integrated Security=True;";

            string query = "INSERT INTO Student (Id, Name, Marks) VALUES (@Id, @Name, @Marks)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Marks", marks);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Inserted Successfully");
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Database Error: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}