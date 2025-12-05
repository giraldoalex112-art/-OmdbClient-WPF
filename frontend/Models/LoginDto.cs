namespace OmdbClient.Models
{
    /// <summary>
    /// DTO para enviar credenciales de login al servidor.
    /// </summary>
    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}