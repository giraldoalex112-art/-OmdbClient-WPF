using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using OmdbApi.Data;
using OmdbApi.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuración de EF Core con SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

// 2. Swagger para documentación y pruebas
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. Activar detección de controladores
builder.Services.AddControllers();

// 4. Configuración de JWT
var jwt = builder.Configuration.GetSection("Jwt");
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));

builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = "JwtBearer";
    o.DefaultChallengeScheme = "JwtBearer";
}).AddJwtBearer("JwtBearer", o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwt["Issuer"],
        ValidAudience = jwt["Audience"],
        IssuerSigningKey = key
    };
});

builder.Services.AddAuthorization();

// 5. Cliente HTTP para OMDb
builder.Services.AddHttpClient("Omdb", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["Omdb:BaseUrl"]!);
});

// 6. Registrar servicios propios
builder.Services.AddSingleton<OmdbService>();

var app = builder.Build();

// 7. Configuración de Swagger en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 8. Middleware de autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// 9. Activar rutas de controladores
app.MapControllers();

// 10. Endpoint raíz de prueba
app.MapGet("/", () => "OMDb API Backend funcionando 🚀");

app.Run();