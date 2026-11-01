public interface IGeracaoDeImagensService
{
    Task<string> GerarImagemAsync(string prompt);
}