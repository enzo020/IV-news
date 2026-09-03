namespace IVnews.Model
{
    public class Noticia
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Resumo { get; set; }
        public string Classificacao { get; set; }
        public DateTime PublicadoEm { get; set; }
    }
}