using Microsoft.AspNetCore.Mvc;
using StaffSphere.DTOs;
using StaffSphere.Services;

namespace StaffSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(IEmployeeService employeeService, ILogger<EmployeesController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        // GET: api/employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            _logger.LogInformation("Fetching all employees at {Time}", DateTime.UtcNow);
            return Ok(await _employeeService.GetAllEmployeesAsync());
        }

        // GET: api/employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        // POST: api/employees
        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateEmployee(CreateEmployeeDto dto)
        {
            var result = await _employeeService.CreateEmployeeAsync(dto);
            return CreatedAtAction(nameof(GetEmployee), new { id = result.EmployeeId }, result);
        }

        // PUT: api/employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, CreateEmployeeDto dto)
        {
            var success = await _employeeService.UpdateEmployeeAsync(id, dto);
            if (!success)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/employees/5
        [HttpDelete("{id}")]
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var success = await _employeeService.DeleteEmployeeAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}