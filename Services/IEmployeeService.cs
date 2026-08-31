using StaffSphere.DTOs;

namespace StaffSphere.Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetAllEmployeesAsync();
        Task<EmployeeDto?> GetEmployeeByIdAsync(int id);
        Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto dto);
        Task<bool> UpdateEmployeeAsync(int id, CreateEmployeeDto dto);
        Task<bool> DeleteEmployeeAsync(int id);
    }
}
