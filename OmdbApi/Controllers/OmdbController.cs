using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; // ✅ Necesario para [AllowAnonymous]
using System.Net.Http;
using System.Threading.Tasks;

namespace OmdbApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OmdbController : ControllerBase
    {
        [HttpGet("buscar")]
        [AllowAnonymous] // Permite acceso sin autenticación
        public async Task<IActionResult> BuscarEnOmdb([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("La consulta está vacía.");

            var apiKey = "e353a2fc"; // ⚠️ No subir esto al repositorio
            var url = $"https://www.omdbapi.com/?apikey={apiKey}&t={Uri.EscapeDataString(query)}";

            using var http = new HttpClient();
            var response = await http.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, "Error al consultar OMDb.");

            var json = await response.Content.ReadAsStringAsync();
            return Content(json, "application/json");
        }
    }
}