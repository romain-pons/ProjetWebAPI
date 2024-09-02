using Microsoft.EntityFrameworkCore;
using ProjetWebAPI.Models;

namespace ProjetWebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Etudiants> Etudiants { get; set; }
        public DbSet<Cours> Cours { get; set; }
        public DbSet<Profs> Profs { get; set; }
        

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Ici, vous pouvez ajouter des configurations spécifiques pour vos modèles, comme des contraintes, des index, etc.
        }
    }
}
