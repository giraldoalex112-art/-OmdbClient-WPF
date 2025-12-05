using OmdbApi.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace OmdbApi.Services
{
    public class OmdbService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public OmdbService(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            // Cliente HTTP configurado en Program.cs para apuntar a OMDb
            _httpClient = httpClientFactory.CreateClient("Omdb");

            // API Key tomada desde appsettings.json
            _apiKey = config["Omdb:ApiKey"] ?? throw new ArgumentNullException("Omdb:ApiKey");
        }

        /// <summary>
        /// Busca películas por título usando la API de OMDb.
        /// </summary>
        public async Task<OmdbSearchResult?> SearchMoviesAsync(string title)
        {
            if (string.IsNullOrWhiteSpace(title)) return null;

            var response = await _httpClient.GetAsync($"?s={title}&apikey={_apiKey}");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<OmdbSearchResult>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        /// <summary>
        /// Obtiene el detalle de una película por ImdbID usando la API de OMDb.
        /// </summary>
        public async Task<MovieDetail?> GetMovieDetailAsync(string imdbId)
        {
            if (string.IsNullOrWhiteSpace(imdbId)) return null;

            var response = await _httpClient.GetAsync($"?i={imdbId}&apikey={_apiKey}");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();

            var detalle = JsonSerializer.Deserialize<MovieDetail>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return detalle;
        }
    }
}