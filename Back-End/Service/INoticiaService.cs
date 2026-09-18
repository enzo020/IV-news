namespace IVNews.Services
{
    public interface INoticiaService
    {
        Task<string> ObterNoticiasDaApiAsync();
    }
}