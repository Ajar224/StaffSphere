using Microsoft.Data.SqlClient;
using MiniHRMS.Data;
using MiniHRMS.Models;

namespace MiniHRMS.Services
{
    public class AttendanceService
    {
        public void MarkAttendance(Attendance att)
        {
            using var connection = DbConnection.GetConnection();
            connection.Open();

            string query = @"INSERT INTO Attendance (EmployeeId, AttendanceDate, CheckIn, CheckOut, Status) 
                              VALUES (@EmployeeId, @AttendanceDate, @CheckIn, @CheckOut, @Status)";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@EmployeeId", att.EmployeeId);
            command.Parameters.AddWithValue("@AttendanceDate", att.AttendanceDate);
            command.Parameters.AddWithValue("@CheckIn", att.CheckIn);
            command.Parameters.AddWithValue("@CheckOut", att.CheckOut);
            command.Parameters.AddWithValue("@Status", att.Status);

            command.ExecuteNonQuery();
            Console.WriteLine("✅ Attendance marked successfully!");
        }

        public List<Attendance> GetAttendanceByEmployee(int employeeId)
        {
            var records = new List<Attendance>();

            using var connection = DbConnection.GetConnection();
            connection.Open();

            string query = "SELECT * FROM Attendance WHERE EmployeeId = @EmployeeId ORDER BY AttendanceDate";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@EmployeeId", employeeId);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var att = new Attendance
                {
                    AttendanceId = reader.GetInt32(reader.GetOrdinal("AttendanceId")),
                    EmployeeId = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                    AttendanceDate = reader.GetDateTime(reader.GetOrdinal("AttendanceDate")),
                    CheckIn = reader.GetTimeSpan(reader.GetOrdinal("CheckIn")),
                    CheckOut = reader.GetTimeSpan(reader.GetOrdinal("CheckOut")),
                    Status = reader.GetString(reader.GetOrdinal("Status"))
                };
                records.Add(att);
            }

            return records;
        }
    }
}
