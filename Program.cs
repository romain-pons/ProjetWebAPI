using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using ProjetWebAPI.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Ajouter les services à l'injection de dépendances
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "V1",
        Title = "SchoolDB API",
        Description = "A simple example ASP.NET Core Web API",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Julien LAY - Romain PONS - Adrien AUTEF",
            Email = "julien.lay@edu.igensia.com; romain.pons@edu.igensia.com; adrien.autef@edu.igensia.com",
            Url = new Uri("https://github.com/romain-pons/ProjetWebAPI")
        }
    });

    // Inclure le fichier XML de documentation
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


// Configuration de DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 21))
    )
);

// Ajouter les services personnalisés
builder.Services.AddScoped<EtudiantsService>();
builder.Services.AddScoped<ProfsService>();
builder.Services.AddScoped<CoursService>();

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
