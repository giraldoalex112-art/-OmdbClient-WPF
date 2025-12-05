namespace BackendAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }                // Clave primaria
        public string Nombre { get; set; } = "";   // Nombre del usuario
        public string Email { get; set; } = "";    // Correo electrónico
    }
}