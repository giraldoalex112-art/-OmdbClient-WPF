using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using OmdbClient.Models;

namespace OmdbClient.Services
{
    /// <summary>
    /// Cliente HTTP para consumir la API del servidor.
    /// Maneja autenticación, registro y gestión de favoritas.
    /// </summary>
    public class ApiClient
    {
        private readonly HttpClient _http;
        private string? _token;

        public ApiClient(string baseUrl)
        {
            _http = new HttpClient { BaseAddress = new Uri(baseUrl) };
        }

        public void SetToken(string token)
        {
            _token = token;
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<AuthResponse?> LoginAsync(LoginDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/Auth/Login", dto);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<AuthResponse>();
        }

        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/Auth/Register", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<FavoritaDto>> GetFavoritasAsync()
        {
            return await _http.GetFromJsonAsync<List<FavoritaDto>>("api/Favoritas")
                   ?? new List<FavoritaDto>();
        }

        public async Task<bool> GuardarFavoritaAsync(FavoritaDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/Favoritas", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarFavoritaAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/Favoritas/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<FavoritaDto?> BuscarFavoritaAsync(string query)
        {
            var encoded = Uri.EscapeDataString(query);
            var response = await _http.GetAsync($"api/Favoritas/buscar?query={encoded}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<FavoritaDto>();
        }

        /// <summary>
        /// GET /api/Omdb/buscar?query=texto
        /// Busca una película en OMDb por título.
        /// </summary>
        public async Task<OmdbMovieDto?> BuscarEnOmdbAsync(string query)
        {
            var encoded = Uri.EscapeDataString(query);
            var response = await _http.GetAsync($"api/Omdb/buscar?query={encoded}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<OmdbMovieDto>();
        }
    }
}