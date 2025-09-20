using Microsoft.EntityFrameworkCore;
using MusicStore.Persistence;
using MusicStore.Repositories;

// Services and configuration for the MusicStore API

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuring context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // SQL Server
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Register the GenreRepository as a singleton service
builder.Services.AddScoped<IGenreRepository, GenreRepository>();

// AddSingleton - Se crea una sola instancia de la clase y se comparte en toda la aplicacion, cuando se trabaja con objetos en memoria
// AddScoped - Se crea una nueva instancia por cada peticion HTTP, cuando se trabaja con BD
// AddTransient - Se crea una nueva instancia cada vez que se inyecta el servicio

// Middleware configuration

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
