namespace IVnews.Model
{
    public class Resumo
    {
        public int Id { get; set; }
        public string Texto { get; set; }

        // Relacionamento com Noticia
        public int NoticiaId { get; set; }
        public Noticia Noticia { get; set; }
    }
}