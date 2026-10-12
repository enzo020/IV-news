using System.ComponentModel.DataAnnotations;

namespace IVnews.Model
{
    public class Resumo
    {
        public int Id { get; set; }
        public string? Texto { get; set; }

        // Resultados do Gemini (ficam vazios até a notícia ser processada)
        public string? Titulo { get; set; }
        public string? ResumoExecutivo { get; set; }
        public List<string>? PontosChave { get; set; }
        public string? Categoria { get; set; }

        [RegularExpression("^(Positivo|Negativo|Neutro)$")]
        public string? Sentimento { get; set; }

        public int NoticiaId { get; set; }
        public Noticia? Noticia { get; set; }
    }
}