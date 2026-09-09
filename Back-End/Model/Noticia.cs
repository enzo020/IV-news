namespace IVnews.Model
{
    public class Noticia
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Conteudo { get; set; }
        public string? Autor { get; set; }
        public string? Fonte { get; set; }
        public string? ImagemUrl { get; set; }
        public DateTime PublicadoEm { get; set; }

        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        public int LocalizacaoId { get; set; }
        public Localizacao? Localizacao { get; set; }
    }
}