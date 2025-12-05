using Microsoft.EntityFrameworkCore;
using OmdbApi.Models;

namespace OmdbApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<PeliculaFavorita> PeliculasFavoritas => Set<PeliculaFavorita>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Usuario>().HasIndex(u => u.Email).IsUnique();
        base.OnModelCreating(b);
    }
}