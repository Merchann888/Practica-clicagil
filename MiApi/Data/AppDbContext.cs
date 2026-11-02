using Microsoft.EntityFrameworkCore;
using MiApi.Models;

namespace MiApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<Proyecto> Proyectos => Set<Proyecto>();
        public DbSet<Tecnologia> Tecnologias => Set<Tecnologia>();
    }
}