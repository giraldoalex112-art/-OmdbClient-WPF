namespace OmdbApi.DTOs
{
    /// <summary>
    /// DTO para representar una película favorita.
    /// Se utiliza en las respuestas y peticiones de la API.
    /// </summary>
    public class FavoritaDto
    {
        /// <summary>
        /// Identificador único de la película favorita en la base de datos.
        /// </summary>
        public int Id { get; set; }   // ✅ Necesario para editar/eliminar

        /// <summary>
        /// Identificador de la película en IMDb.
        /// </summary>
        public string ImdbID { get; set; } = string.Empty;

        /// <summary>
        /// Título de la película.
        /// </summary>
        public string Titulo { get; set; } = string.Empty;

        /// <summary>
        /// Año de lanzamiento.
        /// </summary>
        public string Year { get; set; } = string.Empty;

        /// <summary>
        /// URL del póster de la película.
        /// </summary>
        public string? Poster { get; set; }
    }
}