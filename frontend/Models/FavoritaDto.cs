namespace OmdbClient.Models
{
    public class FavoritaDto
    {
        public int Id { get; set; }

        // Todas las propiedades inicializadas para evitar null
        public string ImdbID { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public string Poster { get; set; } = string.Empty;
    }
}