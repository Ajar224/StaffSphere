using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaffSphere.Data;
using StaffSphere.DTOs;
using StaffSphere.Models;
using StaffSphere.Services;

namespace StaffSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;

        public AuthController(ApplicationDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Username == dto.Username))
                return BadRequest("Username already exists.");

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = _tokenService.CreateToken(user);
            return Ok(new AuthResponseDto { Token = token, Username = user.Username, Role = user.Role });
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized("Invalid username or password.");

            var token = _tokenService.CreateToken(user);
            return Ok(new AuthResponseDto { Token = token, Username = user.Username, Role = user.Role });
        }
    }
}
