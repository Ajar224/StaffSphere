using Microsoft.EntityFrameworkCore;
using StaffSphere.Models;

namespace StaffSphere.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<User> Users { get; set; }
    }
}