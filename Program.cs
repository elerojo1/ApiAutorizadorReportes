using ApiAutorizadorReportes.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- Swagger ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // Necesario para encontrar los endpoints
builder.Services.AddSwaggerGen();           // Genera el archivo swagger.json
// --------------------

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

var app = builder.Build();

// --- AGREGAR ESTO ---
if (app.Environment.IsDevelopment()) // Solo se muestra en desarrollo
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Autorizacion Reportes V1");
        // Esto hace que Swagger sea la página de inicio (opcional)
        c.RoutePrefix = string.Empty; 
    });
}
// --------------------

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();