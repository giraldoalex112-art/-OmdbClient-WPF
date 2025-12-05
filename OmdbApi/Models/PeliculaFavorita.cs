namespace OmdbApi.Models;

public class PeliculaFavorita
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public string ImdbID { get; set; } = default!;
    public string Titulo { get; set; } = default!;
    public string Year { get; set; } = default!;
    public string? Poster { get; set; }

    public Usuario? Usuario { get; set; }
}