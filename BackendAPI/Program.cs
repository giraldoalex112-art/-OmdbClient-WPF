using BackendAPI.Data;                     // Contexto de base de datos
using Microsoft.EntityFrameworkCore;       // EF Core
using Microsoft.OpenApi.Models;            // Swagger

var builder = WebApplication.CreateBuilder(args);

//  Configuración de EF Core con SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=usuarios.db"));

//  Agregar controladores
builder.Services.AddControllers();

//  Swagger (documentación de la API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BackendAPI",
        Version = "v1",
        Description = "API para gestión de usuarios con SQLite y EF Core"
    });
});

var app = builder.Build();

//  Configuración del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BackendAPI v1");
        c.RoutePrefix = string.Empty; // Swagger en la raíz (http://localhost:5000/)
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();