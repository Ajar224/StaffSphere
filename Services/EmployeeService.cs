using Microsoft.Data.SqlClient;
using StaffSphere.Data;
using StaffSphere.Models;

namespace StaffSphere.Services
{
    public class EmployeeService
    {
        public void AddEmployee(Employee emp)
        {
            using var connection = DbConnection.GetConnection();
            connection.Open();

            string query = @"INSERT INTO Employees (FullName, Department, Designation, JoiningDate, Email) 
                              VALUES (@FullName, @Department, @Designation, @JoiningDate, @Email)";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FullName", emp.FullName);
            command.Parameters.AddWithValue("@Department", emp.Department);
            command.Parameters.AddWithValue("@Designation", emp.Designation);
            command.Parameters.AddWithValue("@JoiningDate", emp.JoiningDate);
            command.Parameters.AddWithValue("@Email", emp.Email);

            command.ExecuteNonQuery();
            Console.WriteLine("✅ Employee added successfully!");
        }

        public List<Employee> GetAllEmployees()
        {
            var employees = new List<Employee>();

            using var connection = DbConnection.GetConnection();
            connection.Open();

            string query = "SELECT * FROM Employees";
            using var command = new SqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var emp = new Employee
                {
                    EmployeeId = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                    FullName = reader.GetString(reader.GetOrdinal("FullName")),
                    Department = reader.GetString(reader.GetOrdinal("Department")),
                    Designation = reader.GetString(reader.GetOrdinal("Designation")),
                    JoiningDate = reader.GetDateTime(reader.GetOrdinal("JoiningDate")),
                    Email = reader.GetString(reader.GetOrdinal("Email"))
                };
                employees.Add(emp);
            }

            return employees;
        }

        public void UpdateEmployee(int id, Employee emp)
        {
            using var connection = DbConnection.GetConnection();
            connection.Open();

            string query = @"UPDATE Employees 
                              SET FullName = @FullName, Department = @Department, 
                                  Designation = @Designation, JoiningDate = @JoiningDate, Email = @Email
                              WHERE EmployeeId = @EmployeeId";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FullName", emp.FullName);
            command.Parameters.AddWithValue("@Department", emp.Department);
            command.Parameters.AddWithValue("@Designation", emp.Designation);
            command.Parameters.AddWithValue("@JoiningDate", emp.JoiningDate);
            command.Parameters.AddWithValue("@Email", emp.Email);
            command.Parameters.AddWithValue("@EmployeeId", id);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
                Console.WriteLine("✅ Employee updated successfully!");
            else
                Console.WriteLine("❌ No employee found with that ID.");
        }

        public void DeleteEmployee(int id)
        {
            using var connection = DbConnection.GetConnection();
            connection.Open();

            string query = "DELETE FROM Employees WHERE EmployeeId = @EmployeeId";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@EmployeeId", id);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
                Console.WriteLine("✅ Employee deleted successfully!");
            else
                Console.WriteLine("❌ No employee found with that ID.");
        }
    }
}