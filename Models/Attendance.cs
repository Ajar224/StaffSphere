namespace MiniHRMS.Models
{
    public class Attendance
    {
        public int AttendanceId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public TimeSpan CheckIn { get; set; }
        public TimeSpan CheckOut { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}