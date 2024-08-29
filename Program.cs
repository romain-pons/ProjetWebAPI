var builder = WebApplication.CreateBuilder(args);

// Ajouter les services à l'injection de dépendances
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuration de la connexion à la base de données MySQL
builder.Services.AddDbContext<YourDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));

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
