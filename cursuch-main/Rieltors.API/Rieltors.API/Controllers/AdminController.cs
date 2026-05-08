using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rieltors.API.Data;
using Rieltors.API.Models;
using Rieltors.API.Services;

namespace Rieltors.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public AdminController(ApplicationDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AdminResponseDto>> Register(RegisterAdminDto registerDto)
        {
            if (await _context.Admins.AnyAsync(a => a.Username == registerDto.Username))
                return BadRequest(new { message = "Username already exists" });

            if (await _context.Admins.AnyAsync(a => a.Email == registerDto.Email))
                return BadRequest(new { message = "Email already exists" });

            var admin = new Admin
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                Username = registerDto.Username,
                PasswordHash = _passwordHasher.HashPassword(registerDto.Password),
                Phone = registerDto.Phone,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();

            var response = new AdminResponseDto
            {
                Id = admin.Id,
                FullName = admin.FullName,
                Email = admin.Email,
                Username = admin.Username,
                Phone = admin.Phone,
                Role = admin.Role,
                CreatedAt = admin.CreatedAt
            };

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AdminResponseDto>> Login(LoginDto loginDto)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Username == loginDto.Username);

            if (admin == null)
                return Unauthorized(new { message = "Invalid username or password" });

            if (!_passwordHasher.VerifyPassword(loginDto.Password, admin.PasswordHash))
                return Unauthorized(new { message = "Invalid username or password" });

            admin.LastLoginAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            var response = new AdminResponseDto
            {
                Id = admin.Id,
                FullName = admin.FullName,
                Email = admin.Email,
                Username = admin.Username,
                Phone = admin.Phone,
                Role = admin.Role,
                CreatedAt = admin.CreatedAt
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminResponseDto>>> GetAdmins()
        {
            var admins = await _context.Admins
                .Select(a => new AdminResponseDto
                {
                    Id = a.Id,
                    FullName = a.FullName,
                    Email = a.Email,
                    Username = a.Username,
                    Phone = a.Phone,
                    Role = a.Role,
                    CreatedAt = a.CreatedAt
                })
                .ToListAsync();

            return Ok(admins);
        }
    }
}