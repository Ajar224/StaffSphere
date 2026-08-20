namespace MiniHRMS.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
        public DateTime JoiningDate { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}