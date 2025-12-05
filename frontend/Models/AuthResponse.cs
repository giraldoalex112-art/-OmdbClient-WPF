namespace OmdbClient.Models
{
    /// <summary>
    /// Representa la respuesta de autenticación del servidor.
    /// Contiene el token JWT, el correo electrónico del usuario
    /// y el identificador único del usuario.
    /// </summary>
    public class AuthResponse
    {
        /// <summary>
        /// Token JWT devuelto por el servidor.
        /// Obligatorio: nunca debe ser null.
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Correo electrónico del usuario autenticado.
        /// Obligatorio: nunca debe ser null.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Identificador único del usuario autenticado.
        /// </summary>
        public int UserId { get; set; }
    }
}