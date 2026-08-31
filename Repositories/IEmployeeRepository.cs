using StaffSphere.Models;

namespace StaffSphere.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(Employee employee);
        Task<bool> ExistsAsync(int id);
        Task SaveChangesAsync();
    }
}