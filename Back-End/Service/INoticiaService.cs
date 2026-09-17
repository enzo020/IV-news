using IVnews.Model;

namespace IVNews.Services
{
    public interface INoticiaService
    {
        Task<IEnumerable<Noticia>> ObterNoticiasDaApiAsync();
        Task SincronizarNoticiasAsync();
    }
}