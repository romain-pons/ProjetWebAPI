using Microsoft.EntityFrameworkCore;
using ProjetWebAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Ajouter les services à l'injection de dépendances
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuration de DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 21))
    )
);

// Ajouter les services personnalisés
builder.Services.AddScoped<EtudiantService>();
builder.Services.AddScoped<ProfService>();
builder.Services.AddScoped<CourService>();

var app = builder.Build();

// Configuration de middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
