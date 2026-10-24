using CarRentalApi.Data;
using CarRentalApi.Dtos;
using CarRentalApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    public AuthController(AppDbContext context) => _context = context;

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto dto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            return BadRequest("Email already exists.");

        var user = new User { FullName = dto.FullName, Email = dto.Email, Phone = dto.Phone, Role = "Customer" };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return new UserDto(user.UserId, user.FullName, user.Email, user.Role, user.Phone);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user == null) return Unauthorized("Invalid email.");
        return new UserDto(user.UserId, user.FullName, user.Email, user.Role, user.Phone);
    }
}
