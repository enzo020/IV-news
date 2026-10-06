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

        protected override void OnModelCreating(ModelBuilder modelBuilder){

            modelBuilder.Entity<Categoria>().HasData(
                new Categoria {
                    Id = 1,
                    Nome = "Política",
                    Descricao = "Notícias sobre política nacional e internacional"
                },
                new Categoria {
                    Id = 2,
                    Nome = "Esporte",
                    Descricao = "Notícias sobre esportes em geral"
                },
                new Categoria {
                    Id = 3,
                    Nome = "Tecnologia",
                    Descricao = "Notícias sobre tecnologia e inovação"
                },
                new Categoria {
                    Id = 4,
                    Nome = "Saúde",
                    Descricao = "Notícias sobre saúde e bem-estar"
                },
                new Categoria
                {
                    Id = 5,
                    Nome = "Economia",
                    Descricao = "Notícias sobre economia e mercado financeiro"
                },
                new Categoria
                {
                    Id = 6,
                    Nome = "Outros",
                    Descricao = "Notícias sobre categorias variadas"
                }
            );

            modelBuilder.Entity<Localizacao>().HasData(
                new Localizacao
                {
                    Id = 1,
                    Cidade = "Maringá",
                    Estado = "PR",
                    Pais = "Brasil"
                },
                new Localizacao
                {
                    Id = 2,
                    Cidade = "São Paulo",
                    Estado = "SP",
                    Pais = "Brasil"
                },
                new Localizacao
                {
                    Id = 3,
                    Cidade = "Curitiba",
                    Estado = "PR",
                    Pais = "Brasil"
                },
                new Localizacao
                {
                    Id = 4,
                    Cidade = "Rio de Janeiro",
                    Estado = "RJ",
                    Pais = "Brasil"
                }
            );
            
            modelBuilder.Entity<Noticia>()
                .HasOne(n => n.Categoria)
                .WithMany()
                .HasForeignKey("CategoriaId")
                .IsRequired(false);

            modelBuilder.Entity<Noticia>()
                .HasOne(n => n.Localizacao)
                .WithMany()
                .HasForeignKey("LocalizacaoId")
                .IsRequired(false);

        }
    }
}