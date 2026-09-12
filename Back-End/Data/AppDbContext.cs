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

                //sem contrutor
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

                new Categoria {
                    Id = 5,
                    Nome = "Economia",
                    Descricao = "Notícias sobre economia e mercado financeiro"
                }
            );

            modelBuilder.Entity<Localizacao>().HasData(

                //sem contrutor
                new Localizacao {
                    Id = 1,
                    Cidade = "Maringá",
                    Estado = "PR",
                    Pais = "Brasil"
                },

                new Localizacao {
                    Id = 2,
                    Cidade = "São Paulo",
                    Estado = "SP",
                    Pais = "Brasil"
                },

                new Localizacao {
                    Id = 3,
                    Cidade = "Curitiba",
                    Estado = "PR",
                    Pais = "Brasil"
                },

                new Localizacao {
                    Id = 4,
                    Cidade = "Rio de Janeiro",
                    Estado = "RJ",
                    Pais = "Brasil"
                }
            );

            modelBuilder.Entity<Noticia>().HasData(
                new Noticia {
                    Id = 1,
                    Titulo = "Notícia de teste",
                    Conteudo = "Conteúdo de exemplo pra testar o banco.",
                    Autor = "Redação IV News",
                    Fonte = "IV News",
                    ImagemUrl = "https://picsum.photos/600/400?random=5",
                    PublicadoEm = new DateTime(2026, 9, 3),
                    CategoriaId = 3,
                    LocalizacaoId = 1
                },

                new Noticia {
                    Id = 2,
                    Titulo = "Eleições municipais se aproximam",
                    Conteudo = "Candidatos iniciam campanhas em todo o estado.",
                    Autor = "Ana Silva",
                    Fonte = "IV News",
                    ImagemUrl = "https://picsum.photos/600/400?random=4",
                    PublicadoEm = new DateTime(2026, 9, 4),
                    CategoriaId = 1,
                    LocalizacaoId = 2
                },

                new Noticia {
                    Id = 3,
                    Titulo = "Time local vence campeonato",
                    Conteudo = "Vitória histórica na final do estadual.",
                    Autor = "Carlos Souza",
                    Fonte = "IV News",
                    ImagemUrl = "https://picsum.photos/600/400?random=1",
                    PublicadoEm = new DateTime(2026, 9, 5),
                    CategoriaId = 2,
                    LocalizacaoId = 3
                },

                new Noticia {
                    Id = 4,
                    Titulo = "Nova IA promete revolucionar diagnósticos",
                    Conteudo = "Startup lança ferramenta de saúde com inteligência artificial.",
                    Autor = "Beatriz Lima",
                    Fonte = "IV News",
                    ImagemUrl = "https://picsum.photos/600/400?random=2",
                    PublicadoEm = new DateTime(2026, 9, 6),
                    CategoriaId = 3,
                    LocalizacaoId = 4
                },

                new Noticia {
                    Id = 5,
                    Titulo = "Mercado financeiro reage a nova taxa",
                    Conteudo = "Bolsa de valores tem alta após anúncio do governo.",
                    Autor = "Rafael Costa",
                    Fonte = "IV News",
                    ImagemUrl = "https://picsum.photos/600/400?random=3",
                    PublicadoEm = new DateTime(2026, 9, 7),
                    CategoriaId = 5,
                    LocalizacaoId = 1
                }
            );

            modelBuilder.Entity<Resumo>().HasData(
                new Resumo {
                    Id = 1,
                    Texto = "Resumo de exemplo da notícia de teste.",
                    NoticiaId = 1
                },

                new Resumo {
                    Id = 2,
                    Texto = "Candidatos disputam eleições municipais em todo o estado.",
                    NoticiaId = 2
                },

                new Resumo {
                    Id = 3,
                    Texto = "Time conquista título estadual em final emocionante.",
                    NoticiaId = 3
                },

                new Resumo {
                    Id = 4,
                    Texto = "Startup inova com IA aplicada à saúde.",
                    NoticiaId = 4
                },

                new Resumo {
                    Id = 5,
                    Texto = "Governo anuncia nova taxa e mercado reage positivamente.",
                    NoticiaId = 5
                }
            );
        }
    }
}