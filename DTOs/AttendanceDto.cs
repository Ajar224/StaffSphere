namespace StaffSphere.DTOs
{
    public class AttendanceDto
    {
        public int AttendanceId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public DateTime AttendanceDate { get; set; }
        public TimeSpan CheckIn { get; set; }
        public TimeSpan CheckOut { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}