using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using StaffSphere.Data;
using StaffSphere.Models;
using StaffSphere.DTOs;

namespace StaffSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AttendanceController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/attendance
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttendanceDto>>> GetAttendance()
        {
            var records = await _context.Attendances
                .Include(a => a.Employee)
                .ToListAsync();

            return Ok(_mapper.Map<List<AttendanceDto>>(records));
        }

        // GET: api/attendance/employee/5
        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<AttendanceDto>>> GetAttendanceForEmployee(int employeeId)
        {
            var records = await _context.Attendances
                .Include(a => a.Employee)
                .Where(a => a.EmployeeId == employeeId)
                .ToListAsync();

            return Ok(_mapper.Map<List<AttendanceDto>>(records));
        }

        // POST: api/attendance
        [HttpPost]
        public async Task<ActionResult<AttendanceDto>> CreateAttendance(CreateAttendanceDto dto)
        {
            var employeeExists = await _context.Employees.AnyAsync(e => e.EmployeeId == dto.EmployeeId);
            if (!employeeExists)
                return BadRequest($"Employee with ID {dto.EmployeeId} does not exist.");

            var attendance = _mapper.Map<Attendance>(dto);
            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync();

            await _context.Entry(attendance).Reference(a => a.Employee).LoadAsync();
            var resultDto = _mapper.Map<AttendanceDto>(attendance);

            return CreatedAtAction(nameof(GetAttendance), new { id = attendance.AttendanceId }, resultDto);
        }

        // DELETE: api/attendance/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendance(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null)
                return NotFound();

            _context.Attendances.Remove(attendance);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}