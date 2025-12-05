using Microsoft.AspNetCore.Mvc;
using OmdbApi.Models;
using OmdbApi.Services;
using System.Threading.Tasks;

namespace OmdbApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly OmdbService _omdbService;

        public MoviesController(OmdbService omdbService)
        {
            _omdbService = omdbService;
        }

        // GET /api/Movies/Search?title=Matrix
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return BadRequest("Debe ingresar un título.");

            var result = await _omdbService.SearchMoviesAsync(title);

            if (result == null || result.Response == "False")
                return NotFound("No se encontraron películas.");

            return Ok(result);
        }

        // GET /api/Movies/Detalle/{imdbId}
        [HttpGet("Detalle/{imdbId}")]
        public async Task<IActionResult> Detalle(string imdbId)
        {
            if (string.IsNullOrWhiteSpace(imdbId))
                return BadRequest("Debe ingresar un ImdbID válido.");

            var detalle = await _omdbService.GetMovieDetailAsync(imdbId);

            if (detalle == null || detalle.Response == "False")
                return NotFound($"No se encontró detalle para {imdbId}");

            return Ok(detalle);
        }
    }
}