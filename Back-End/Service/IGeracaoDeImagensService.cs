public interface IGeracaoDeImagensService
{
    Task<byte[]> GerarImagemAsync(string prompt);
}