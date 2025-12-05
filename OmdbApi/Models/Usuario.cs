namespace OmdbApi.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
}