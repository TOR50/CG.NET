using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using mvc2.Models;

namespace mvc2.Controllers
{
    public class StudentController : Controller
    {
        private readonly IConfiguration _configuration;

        public StudentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            List<StudentModel> students = new List<StudentModel>();

            try
            {
                string connString = _configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string sql = "SELECT Id, Name, Age, City FROM StudentsMaster";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    conn.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            students.Add(new StudentModel
                            {
                                Id = Convert.ToInt32(rdr["Id"]),
                                Name = rdr["Name"].ToString(),
                                Age = Convert.ToInt32(rdr["Age"]),
                                City = rdr["City"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error retrieving student data: {ex.Message}";
            }

            return View(students);
        }
    }
}