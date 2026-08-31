using AutoMapper;
using StaffSphere.DTOs;
using StaffSphere.Models;
using StaffSphere.Repositories;

namespace StaffSphere.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _repository.GetAllAsync();
            return _mapper.Map<List<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);
            return employee == null ? null : _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto dto)
        {
            var employee = _mapper.Map<Employee>(dto);
            await _repository.AddAsync(employee);
            await _repository.SaveChangesAsync();
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<bool> UpdateEmployeeAsync(int id, CreateEmployeeDto dto)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null)
                return false;

            _mapper.Map(dto, employee);
            await _repository.UpdateAsync(employee);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null)
                return false;

            await _repository.DeleteAsync(employee);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}