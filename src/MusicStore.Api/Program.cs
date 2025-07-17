using MusicStore.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the GenreRepository as a singleton service
builder.Services.AddSingleton<GenreRepository>(); // Se va a mantener una misma instancia durante toda la vida util de la aplicacion

// AddScoped - Se crea una nueva instancia por cada peticion HTTP
// AddTransient - Se crea una nueva instancia cada vez que se inyecta el servicio

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
