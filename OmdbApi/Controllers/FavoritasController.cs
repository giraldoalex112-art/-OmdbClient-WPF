using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using OmdbApi.Data;
using OmdbApi.Models;
using OmdbApi.DTOs;

namespace OmdbApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // 🔒 Protege todos los endpoints con JWT
    public class FavoritasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FavoritasController(AppDbContext context)
        {
            _context = context;
        }

        // Obtener el UsuarioId desde el token JWT
        private int GetUsuarioId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            return int.Parse(userIdClaim!.Value);
        }

        // ✅ NUEVO: GET /api/Favoritas/buscar?query=texto
        [HttpGet("buscar")]
        public async Task<ActionResult<FavoritaDto>> BuscarFavorita([FromQuery] string query)
        {
            var usuarioId = GetUsuarioId();

            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("La consulta está vacía.");

            var favorita = await _context.PeliculasFavoritas
                .FirstOrDefaultAsync(f => f.UsuarioId == usuarioId &&
                                          (f.Titulo.Contains(query) || f.ImdbID == query));

            if (favorita == null)
                return NotFound("No se encontró ninguna coincidencia.");

            var dto = new FavoritaDto
            {
                Id = favorita.Id,
                ImdbID = favorita.ImdbID,
                Titulo = favorita.Titulo,
                Year = favorita.Year,
                Poster = favorita.Poster
            };

            return Ok(dto);
        }

        // GET /api/Favoritas
        [HttpGet]
        public async Task<IActionResult> GetFavoritas()
        {
            var usuarioId = GetUsuarioId();
            var favoritas = await _context.PeliculasFavoritas
                .Where(f => f.UsuarioId == usuarioId)
                .ToListAsync();

            return Ok(favoritas);
        }

        // POST /api/Favoritas
        [HttpPost]
        public async Task<IActionResult> AddFavorita([FromBody] FavoritaDto favoritaDto)
        {
            var usuarioId = GetUsuarioId();

            var favorita = new PeliculaFavorita
            {
                UsuarioId = usuarioId,
                ImdbID = favoritaDto.ImdbID,
                Titulo = favoritaDto.Titulo,
                Year = favoritaDto.Year,
                Poster = favoritaDto.Poster
            };

            _context.PeliculasFavoritas.Add(favorita);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFavoritas), new { id = favorita.Id }, favorita);
        }

        // DELETE /api/Favoritas/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorita(int id)
        {
            var usuarioId = GetUsuarioId();
            var favorita = await _context.PeliculasFavoritas
                .FirstOrDefaultAsync(f => f.Id == id && f.UsuarioId == usuarioId);

            if (favorita == null)
                return NotFound("No existe o no pertenece al usuario autenticado.");

            _context.PeliculasFavoritas.Remove(favorita);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}