using Microsoft.EntityFrameworkCore;
using IVnews.Model;

namespace IVnews.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Noticia> Noticias { get; set; }
    }
}