using System.ComponentModel.DataAnnotations;

namespace StaffSphere.DTOs
{
    public class CreateAttendanceDto
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime AttendanceDate { get; set; }

        public TimeSpan CheckIn { get; set; }
        public TimeSpan CheckOut { get; set; }

        [Required]
        public string Status { get; set; } = string.Empty;
    }
}