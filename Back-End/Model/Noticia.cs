using System.ComponentModel.DataAnnotations;

namespace IVnews.Model
{
    public class Noticia
    {
        public int Id { get; set; }

        [Required]
        public string? Titulo { get; set; }

        [Required]
        public string? Conteudo { get; set; }

        public string? Autor { get; set; }

        [Required]
        public string? Fonte { get; set; }

        public string? UrlNoticia { get; set; }
        public string? ImagemUrl { get; set; }
        public string? IdExterno { get; set; }
        public string? Idioma { get; set; }

        public DateTime PublicadoEm { get; set; }

        public Categoria? Categoria { get; set; }
        public Localizacao? Localizacao { get; set; }
    }
}