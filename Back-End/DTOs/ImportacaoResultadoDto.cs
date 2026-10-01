namespace IVnews.DTOs.ApiTube
{
    public class ImportacaoResultadoDto
    {
        public int TotalRecebidoDaApi { get; set; }
        public int TotalInserido { get; set; }
        public int TotalIgnoradasDuplicadas { get; set; }
        public string Mensagem { get; set; }
    }
}