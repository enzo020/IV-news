namespace IVnews.Model
{
    public class Noticia
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public DateTime PublicadoEm { get; set; }

        // Relacionamento com Categoria
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        // Relacionamento com Localizacao
        public int LocalizacaoId { get; set; }
        public Localizacao Localizacao { get; set; }
    }
}