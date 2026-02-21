using System.Data.SqlClient;
public class AppointmentRepository
{
    private readonly string _connectionString;

    public AppointmentRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public List<Appointment> GetAppointments()
    {
        var appointments = new List<Appointment>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Appointments", conn);
            conn.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    appointments.Add(new Appointment
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        PatientName = reader["PatientName"].ToString(),
                        DoctorName = reader["DoctorName"].ToString(),
                        AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                        Status = reader["Status"].ToString()
                    });
                }
            }
        }
        return appointments;
    }
}

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=LPU;Database=HospitalDB;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}