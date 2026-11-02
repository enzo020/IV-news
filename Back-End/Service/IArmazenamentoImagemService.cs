public interface IArmazenamentoImagemService 
{
    public Task<string> SalvarImagemAsync(byte[] imagemBytes, string nomeArquivo);
}