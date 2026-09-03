using Microsoft.EntityFrameworkCore;
using IVnews.Model;

namespace IVnews.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Noticia> Noticias { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Localizacao> Localizacoes { get; set; }
        public DbSet<Resumo> Resumos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { Id = 1, Nome = "Política" },
                new Categoria { Id = 2, Nome = "Esporte" },
                new Categoria { Id = 3, Nome = "Tecnologia" }
            );

            modelBuilder.Entity<Localizacao>().HasData(
                new Localizacao { Id = 1, Cidade = "Maringá", Estado = "PR" },
                new Localizacao { Id = 2, Cidade = "São Paulo", Estado = "SP" }
            );

            modelBuilder.Entity<Noticia>().HasData(
                new Noticia
                {
                    Id = 1,
                    Titulo = "Notícia de teste",
                    Conteudo = "Conteúdo de exemplo pra testar o banco.",
                    PublicadoEm = new DateTime(2026, 9, 3),
                    CategoriaId = 3,
                    LocalizacaoId = 1
                }
            );

            modelBuilder.Entity<Resumo>().HasData(
                new Resumo
                {
                    Id = 1,
                    Texto = "Resumo de exemplo da notícia de teste.",
                    NoticiaId = 1
                }
            );
        }
    }
}