public interface IGeminiService
{
    Task<string?> GerarResumoAsync(string conteudo);
    Task<ProcessamentoNoticiaDto> ProcessarNoticiaAsync(string conteudo);
}    
