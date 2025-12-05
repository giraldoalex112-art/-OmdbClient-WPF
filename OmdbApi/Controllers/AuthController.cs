using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OmdbApi.Data;
using OmdbApi.Dtos;
using OmdbApi.Models;
using OmdbApi.Services;

namespace OmdbApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IConfiguration _cfg;

    public AuthController(AppDbContext db, IConfiguration cfg)
    {
        _db = db;
        _cfg = cfg;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
            return BadRequest("Email y Password son obligatorios.");

        var exists = await _db.Usuarios.AnyAsync(u => u.Email == dto.Email);
        if (exists) return Conflict("Email ya registrado.");

        var user = new Usuario
        {
            Email = dto.Email,
            PasswordHash = SecurityService.Hash(dto.Password)
        };

        _db.Usuarios.Add(user);
        await _db.SaveChangesAsync();

        return Created($"/api/Auth/{user.Id}", new { user.Id, user.Email });
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _db.Usuarios.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user is null) return Unauthorized();

        if (user.PasswordHash != SecurityService.Hash(dto.Password))
            return Unauthorized();

        var token = SecurityService.CreateToken(user.Id, user.Email, _cfg);
        return Ok(new { token });
    }
}