using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using OmdbClient.Models;

namespace OmdbClient.Services
{
    public class OmdbService
    {
        private readonly HttpClient _httpClient;

        public OmdbService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        /// <summary>
        /// Obtiene el detalle de una película desde el backend usando el ImdbID.
        /// </summary>
        /// <param name="imdbId">Identificador IMDb de la película</param>
        /// <returns>Objeto DetallePelicula o null si falla</returns>
        public async Task<DetallePelicula?> ObtenerDetalle(string imdbId)
        {
            if (string.IsNullOrWhiteSpace(imdbId))
                throw new ArgumentException("El ImdbID no puede ser nulo o vacío.", nameof(imdbId));

            try
            {
                // ✅ Ruta corregida: apunta al backend en /api/Movies/Detalle/{imdbId}
                var response = await _httpClient
                    .GetAsync($"api/Movies/Detalle/{imdbId}")
                    .ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error HTTP: {response.StatusCode}");
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (string.IsNullOrWhiteSpace(json))
                {
                    Console.WriteLine("La respuesta de la API está vacía.");
                    return null;
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var detalle = JsonSerializer.Deserialize<DetallePelicula>(json, options);

                if (detalle == null)
                {
                    Console.WriteLine("No se pudo mapear el JSON a DetallePelicula.");
                }

                return detalle;
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"Error de conexión HTTP: {httpEx.Message}");
                return null;
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Error al deserializar JSON: {jsonEx.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado al obtener detalle: {ex.Message}");
                return null;
            }
        }
    }
}